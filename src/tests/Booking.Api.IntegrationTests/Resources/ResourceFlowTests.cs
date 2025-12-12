using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Booking.Api.IntegrationTests.Resources;

public class ResourcesFlowTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ResourcesFlowTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetAll_ReturnsSeededResources()
    {
        var resources = await _client.GetFromJsonAsync<List<ResourceResponse>>("/api/Resources");

        Assert.NotNull(resources);
        Assert.NotEmpty(resources!);

        var first = resources![0];
        Assert.NotEqual(Guid.Empty, first.Id);
        Assert.False(string.IsNullOrWhiteSpace(first.Name));
    }

    [Fact]
    public async Task GetById_ReturnsResource()
    {
        // 1) Get list
        var resources = await _client.GetFromJsonAsync<List<ResourceResponse>>("/api/Resources");
        Assert.NotNull(resources);
        Assert.NotEmpty(resources!);

        var id = resources![0].Id;

        // 2) Get by id
        var resp = await _client.GetAsync($"/api/Resources/{id}");
        Assert.Equal(HttpStatusCode.OK, resp.StatusCode);

        var resource = await resp.Content.ReadFromJsonAsync<ResourceResponse>();
        Assert.NotNull(resource);

        Assert.Equal(id, resource!.Id);
        Assert.False(string.IsNullOrWhiteSpace(resource.Name));
    }

    [Fact]
    public async Task GetById_WhenNotFound_Returns404()
    {
        var randomId = Guid.NewGuid();

        var resp = await _client.GetAsync($"/api/Resources/{randomId}");

        Assert.Equal(HttpStatusCode.NotFound, resp.StatusCode);
    }

    private sealed record ResourceResponse(Guid Id, string Name, int Capacity, bool IsActive, string? Description = null);
}
