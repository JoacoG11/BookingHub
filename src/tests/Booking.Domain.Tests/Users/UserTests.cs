using System;
using Booking.Domain.Entities;
using Xunit;

namespace Booking.Domain.Tests.Users;

public class UserTests
{
    [Fact]
    public void CreateUser_WithValidData_SetsProperties()
    {
        // Arrange
        var name = "John Doe";
        var email = "john@doe.com";

        // Act
        var user = new User(name, email);

        // Assert
        Assert.NotEqual(Guid.Empty, user.Id);
        Assert.Equal(name, user.Name);
        Assert.Equal(email, user.Email);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void CreateUser_WithInvalidName_Throws(string? invalidName)
    {
        // Arrange
        var email = "john@doe.com";

        // Act + Assert
        Assert.Throws<ArgumentException>(() => new User(invalidName!, email));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void CreateUser_WithInvalidEmail_Throws(string? invalidEmail)
    {
        // Arrange
        var name = "John Doe";

        // Act + Assert
        Assert.Throws<ArgumentException>(() => new User(name, invalidEmail!));
    }

    [Fact]
    public void UpdateName_WithValidName_Updates()
    {
        // Arrange
        var user = new User("John Doe", "john@doe.com");

        // Act
        user.UpdateName("Jane Doe");

        // Assert
        Assert.Equal("Jane Doe", user.Name);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void UpdateName_WithInvalidName_Throws(string? invalidName)
    {
        // Arrange
        var user = new User("John Doe", "john@doe.com");

        // Act + Assert
        Assert.Throws<ArgumentException>(() => user.UpdateName(invalidName!));
    }
}
