using System;
using Xunit;

using Booking.Domain.Entities;
using BookingEntity = Booking.Domain.Entities.Booking;

namespace Booking.Domain.Tests;

public class BookingTests
{
    [Fact]
    public void CreateBooking_WithValidData_SetsProperties()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var resourceId = Guid.NewGuid();
        var start = new DateTime(2025, 12, 11, 10, 0, 0);
        var end   = new DateTime(2025, 12, 11, 11, 0, 0);

        // Act
        var booking = new BookingEntity(userId, resourceId, start, end);

        // Assert
        Assert.Equal(userId, booking.UserId);
        Assert.Equal(resourceId, booking.ResourceId);
        Assert.Equal(start, booking.StartTime);
        Assert.Equal(end, booking.EndTime);
        Assert.Equal(BookingStatus.Pending, booking.Status);
    }

    [Fact]
    public void CreateBooking_WithInvalidRange_Throws()
    {
        var userId = Guid.NewGuid();
        var resourceId = Guid.NewGuid();
        var start = new DateTime(2025, 12, 11, 11, 0, 0);
        var end   = new DateTime(2025, 12, 11, 10, 0, 0);

        Assert.Throws<ArgumentException>(() =>
            new BookingEntity(userId, resourceId, start, end));
    }

    [Fact]
    public void ConfirmBooking_ChangesStatusToConfirmed()
    {
        var booking = new BookingEntity(
            Guid.NewGuid(),
            Guid.NewGuid(),
            new DateTime(2025, 12, 11, 10, 0, 0),
            new DateTime(2025, 12, 11, 11, 0, 0));

        booking.Confirm();

        Assert.Equal(BookingStatus.Confirmed, booking.Status);
    }

    [Fact]
    public void CancelBooking_ChangesStatusToCancelled()
    {
        var booking = new BookingEntity(
            Guid.NewGuid(),
            Guid.NewGuid(),
            new DateTime(2025, 12, 11, 10, 0, 0),
            new DateTime(2025, 12, 11, 11, 0, 0));

        booking.Cancel();

        Assert.Equal(BookingStatus.Cancelled, booking.Status);
    }
}
