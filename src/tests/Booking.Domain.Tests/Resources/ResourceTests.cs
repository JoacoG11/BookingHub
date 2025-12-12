using System;
using Booking.Domain.Entities;
using Xunit;

namespace Booking.Domain.Tests.Resources;

public class ResourceTests
{
    [Fact]
    public void CreateResource_WithValidData_SetsProperties_And_IsActiveTrue()
    {
        // Arrange
        var name = "Room A";
        var capacity = 10;
        var description = "Meeting room";

        // Act
        var resource = new Resource(name, capacity, description);

        // Assert
        Assert.NotEqual(Guid.Empty, resource.Id);
        Assert.Equal(name, resource.Name);
        Assert.Equal(capacity, resource.Capacity);
        Assert.Equal(description, resource.Description);
        Assert.True(resource.IsActive);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void CreateResource_WithInvalidName_Throws(string? invalidName)
    {
        // Arrange
        var capacity = 10;

        // Act + Assert
        Assert.Throws<ArgumentException>(() => new Resource(invalidName!, capacity));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-10)]
    public void CreateResource_WithInvalidCapacity_Throws(int invalidCapacity)
    {
        // Arrange
        var name = "Room A";

        // Act + Assert
        Assert.Throws<ArgumentException>(() => new Resource(name, invalidCapacity));
    }

    [Fact]
    public void Deactivate_SetsIsActiveFalse()
    {
        // Arrange
        var resource = new Resource("Room A", 10);

        // Act
        resource.Deactivate();

        // Assert
        Assert.False(resource.IsActive);
    }

    [Fact]
    public void Activate_SetsIsActiveTrue()
    {
        // Arrange
        var resource = new Resource("Room A", 10);
        resource.Deactivate();

        // Act
        resource.Activate();

        // Assert
        Assert.True(resource.IsActive);
    }
}
