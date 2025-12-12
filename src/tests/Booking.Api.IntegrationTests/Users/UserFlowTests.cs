using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Booking.Api.IntegrationTests.Users;

public class UsersFlowTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public UsersFlowTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetByEmail_ReturnsSeededUser()
    {
        // Act
        var user = await _client.GetFromJsonAsync<UserResponse>(
            "/api/Users/by-email?email=demo@example.com"
        );

        // Assert
        Assert.NotNull(user);
        Assert.NotEqual(Guid.Empty, user!.Id);
        Assert.Equal("demo@example.com", user.Email);
        Assert.False(string.IsNullOrWhiteSpace(user.Name));
    }

    [Fact]
    public async Task GetById_ReturnsSameUserAsByEmail()
    {
        // 1) Get seeded user by email
        var userByEmail = await _client.GetFromJsonAsync<UserResponse>(
            "/api/Users/by-email?email=demo@example.com"
        );

        Assert.NotNull(userByEmail);
        var id = userByEmail!.Id;

        // 2) Get by id
        var byIdResponse = await _client.GetAsync($"/api/Users/{id}");
        Assert.Equal(HttpStatusCode.OK, byIdResponse.StatusCode);

        var userById = await byIdResponse.Content.ReadFromJsonAsync<UserResponse>();
        Assert.NotNull(userById);

        Assert.Equal(userByEmail.Id, userById!.Id);
        Assert.Equal(userByEmail.Email, userById.Email);
        Assert.Equal(userByEmail.Name, userById.Name);
    }

    [Fact]
    public async Task GetById_WhenNotFound_Returns404()
    {
        var randomId = Guid.NewGuid();

        var resp = await _client.GetAsync($"/api/Users/{randomId}");

        Assert.Equal(HttpStatusCode.NotFound, resp.StatusCode);
    }

    private sealed record UserResponse(Guid Id, string Name, string Email);
}
