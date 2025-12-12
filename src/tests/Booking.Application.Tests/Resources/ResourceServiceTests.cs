using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Booking.Application.Resources;
using Booking.Domain.Entities;
using Booking.Domain.Repositories;
using Moq;
using Xunit;

namespace Booking.Application.Tests.Resources;

public class ResourceServiceTests
{
    private readonly Mock<IResourceRepository> _repoMock;
    private readonly ResourceService _service;

    public ResourceServiceTests()
    {
        _repoMock = new Mock<IResourceRepository>();
        _service = new ResourceService(_repoMock.Object);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsMappedDtos()
    {
        var resources = new List<Resource>
        {
            new Resource("Room A", 10, "desc A"),
            new Resource("Room B", 20, "desc B")
        };

        _repoMock
            .Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(resources);

        var result = await _service.GetAllAsync(CancellationToken.None);

        Assert.Equal(2, result.Count);
        Assert.Equal("Room A", result[0].Name);
        Assert.Equal(10, result[0].Capacity);
        Assert.True(result[0].IsActive);
    }

    [Fact]
    public async Task GetByIdAsync_WhenFound_ReturnsDto()
    {
        var id = Guid.NewGuid();
        var resource = new Resource("Demo", 5, "demo desc");

        // Si querés que el Id matchee con "id", deberías poder setearlo en tu entidad.
        // Como tu BaseEntity probablemente setea Id internamente, testeamos por propiedades de negocio.
        _repoMock
            .Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(resource);

        var dto = await _service.GetByIdAsync(id, CancellationToken.None);

        Assert.Equal("Demo", dto.Name);
        Assert.Equal(5, dto.Capacity);
        Assert.True(dto.IsActive);
    }

    [Fact]
    public async Task GetByIdAsync_WhenNotFound_ThrowsKeyNotFound()
    {
        var id = Guid.NewGuid();

        _repoMock
            .Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Resource?)null);

        await Assert.ThrowsAsync<KeyNotFoundException>(() =>
            _service.GetByIdAsync(id, CancellationToken.None));
    }
}
