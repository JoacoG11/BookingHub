using System;
using System.Threading;
using System.Threading.Tasks;
using Booking.Application.Users;
using Booking.Domain.Entities;
using Booking.Domain.Repositories;
using Moq;
using Xunit;

namespace Booking.Application.Tests.Users;

public class UserServiceTests
{
    private readonly Mock<IUserRepository> _repoMock;
    private readonly UserService _service;

    public UserServiceTests()
    {
        _repoMock = new Mock<IUserRepository>();
        _service = new UserService(_repoMock.Object);
    }

    [Fact]
    public async Task GetByEmailAsync_WhenUserExists_ReturnsDto()
    {
        var user = new User("Demo", "demo@example.com");

        _repoMock
            .Setup(r => r.GetByEmailAsync("demo@example.com", It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        var dto = await _service.GetByEmailAsync("demo@example.com", CancellationToken.None);

        Assert.Equal("Demo", dto.Name);
        Assert.Equal("demo@example.com", dto.Email);
    }

    [Fact]
    public async Task GetByEmailAsync_WhenUserDoesNotExist_ThrowsKeyNotFound()
    {
        _repoMock
            .Setup(r => r.GetByEmailAsync("missing@example.com", It.IsAny<CancellationToken>()))
            .ReturnsAsync((User?)null);

        await Assert.ThrowsAsync<KeyNotFoundException>(() =>
            _service.GetByEmailAsync("missing@example.com", CancellationToken.None));
    }

    [Fact]
    public async Task GetByIdAsync_WhenUserDoesNotExist_ThrowsKeyNotFound()
    {
        var id = Guid.NewGuid();

        _repoMock
            .Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>()))
            .ReturnsAsync((User?)null);

        await Assert.ThrowsAsync<KeyNotFoundException>(() =>
            _service.GetByIdAsync(id, CancellationToken.None));
    }
}
