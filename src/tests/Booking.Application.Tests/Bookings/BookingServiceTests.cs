using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Booking.Application.Bookings;
using Booking.Application.Bookings.Commands;
using Booking.Domain.Entities;
using Booking.Domain.Repositories;
using Moq;
using Xunit;

// Alias para evitar el clásico error: "Booking is a namespace but is used like a type"
using BookingEntity = Booking.Domain.Entities.Booking;

namespace Booking.Application.Tests.Bookings;

public class BookingServiceTests
{
    private readonly Mock<IBookingRepository> _repoMock;
    private readonly BookingService _service;

    public BookingServiceTests()
    {
        _repoMock = new Mock<IBookingRepository>();
        _service = new BookingService(_repoMock.Object);
    }

    [Fact]
    public async Task CreateAsync_WhenStartIsAfterEnd_ThrowsArgumentException()
    {
        var req = new CreateBookingRequest(
            UserId: Guid.NewGuid(),
            ResourceId: Guid.NewGuid(),
            StartTime: new DateTime(2025, 12, 11, 11, 0, 0),
            EndTime: new DateTime(2025, 12, 11, 10, 0, 0)
        );

        await Assert.ThrowsAsync<ArgumentException>(() =>
            _service.CreateAsync(req, CancellationToken.None));
    }

    [Fact]
    public async Task CreateAsync_WhenOverlapsWithExistingNonCancelled_ThrowsInvalidOperationException()
    {
        var userId = Guid.NewGuid();
        var resourceId = Guid.NewGuid();

        var req = new CreateBookingRequest(
            UserId: userId,
            ResourceId: resourceId,
            StartTime: new DateTime(2025, 12, 11, 10, 0, 0),
            EndTime: new DateTime(2025, 12, 11, 11, 0, 0)
        );

        var existing = new List<BookingEntity>
        {
            new BookingEntity(
                userId: Guid.NewGuid(),
                resourceId: resourceId,
                startTime: new DateTime(2025, 12, 11, 10, 30, 0),
                endTime: new DateTime(2025, 12, 11, 11, 30, 0)
            )
        };

        _repoMock
            .Setup(r => r.GetByResourceAndRangeAsync(resourceId, req.StartTime, req.EndTime))
            .ReturnsAsync(existing);

        await Assert.ThrowsAsync<InvalidOperationException>(() =>
            _service.CreateAsync(req, CancellationToken.None));

        _repoMock.Verify(r => r.AddAsync(It.IsAny<BookingEntity>()), Times.Never);
    }

    [Fact]
    public async Task CreateAsync_WhenOverlapsOnlyWithCancelled_AllowsCreation()
    {
        var userId = Guid.NewGuid();
        var resourceId = Guid.NewGuid();

        var req = new CreateBookingRequest(
            UserId: userId,
            ResourceId: resourceId,
            StartTime: new DateTime(2025, 12, 11, 10, 0, 0),
            EndTime: new DateTime(2025, 12, 11, 11, 0, 0)
        );

        var cancelled = new BookingEntity(
            userId: Guid.NewGuid(),
            resourceId: resourceId,
            startTime: new DateTime(2025, 12, 11, 10, 30, 0),
            endTime: new DateTime(2025, 12, 11, 11, 30, 0)
        );
        cancelled.Cancel();

        _repoMock
            .Setup(r => r.GetByResourceAndRangeAsync(resourceId, req.StartTime, req.EndTime))
            .ReturnsAsync(new List<BookingEntity> { cancelled });

        _repoMock
            .Setup(r => r.AddAsync(It.IsAny<BookingEntity>()))
            .Returns(Task.CompletedTask);

        var dto = await _service.CreateAsync(req, CancellationToken.None);

        Assert.Equal(userId, dto.UserId);
        Assert.Equal(resourceId, dto.ResourceId);
        Assert.Equal("Pending", dto.Status);

        _repoMock.Verify(r => r.AddAsync(It.IsAny<BookingEntity>()), Times.Once);
    }

    [Fact]
    public async Task GetByUserAsync_MapsEntitiesToDtos()
    {
        var userId = Guid.NewGuid();

        var bookings = new List<BookingEntity>
        {
            new BookingEntity(
                userId: userId,
                resourceId: Guid.NewGuid(),
                startTime: new DateTime(2025, 12, 11, 10, 0, 0),
                endTime: new DateTime(2025, 12, 11, 11, 0, 0)
            )
        };

        _repoMock
            .Setup(r => r.GetByUserAsync(userId))
            .ReturnsAsync(bookings);

        var result = await _service.GetByUserAsync(userId, CancellationToken.None);

        Assert.Single(result);
        Assert.Equal(userId, result[0].UserId);
    }
}
