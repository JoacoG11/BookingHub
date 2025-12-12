using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Booking.Api.IntegrationTests;

public class BookingsFlowTests : IClassFixture<BookingApiFactory>
{
    private readonly HttpClient _client;

    public BookingsFlowTests(BookingApiFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task FullFlow_CreateBooking_Then_GetByUser_ReturnsIt()
    {
        // 1) Obtener userId del seed (demo@example.com)
        var user = await _client.GetFromJsonAsync<UserResponse>(
            "/api/Users/by-email?email=demo@example.com"
        );

        Assert.NotNull(user);
        Assert.NotEqual(Guid.Empty, user!.Id);

        // 2) Obtener resourceId del seed
        var resources = await _client.GetFromJsonAsync<List<ResourceResponse>>("/api/Resources");
        Assert.NotNull(resources);
        Assert.NotEmpty(resources!);

        var resourceId = resources![0].Id;
        Assert.NotEqual(Guid.Empty, resourceId);

        // 3) Crear booking
        var request = new CreateBookingRequest(
            userId: user.Id,
            resourceId: resourceId,
            startTime: new DateTime(2025, 12, 11, 10, 0, 0, DateTimeKind.Unspecified),
            endTime:   new DateTime(2025, 12, 11, 11, 0, 0, DateTimeKind.Unspecified)
        );

        var post = await _client.PostAsJsonAsync("/api/Bookings", request);
        Assert.Equal(HttpStatusCode.Created, post.StatusCode);

        var created = await post.Content.ReadFromJsonAsync<BookingDto>();
        Assert.NotNull(created);
        Assert.Equal(user.Id, created!.UserId);
        Assert.Equal(resourceId, created.ResourceId);

        // 4) Obtener bookings por userId y verificar que el creado aparece
        var list = await _client.GetFromJsonAsync<List<BookingDto>>($"/api/Bookings/user/{user.Id}");
        Assert.NotNull(list);

        Assert.Contains(list!, b => b.Id == created.Id);
    }

    // ===== DTOs mínimos para el test =====

    private sealed record UserResponse(Guid Id, string Name, string Email);

    private sealed record ResourceResponse(Guid Id, string Name, int Capacity, bool IsActive);

    private sealed record CreateBookingRequest(Guid userId, Guid resourceId, DateTime startTime, DateTime endTime);

    private sealed record BookingDto(Guid Id, Guid UserId, Guid ResourceId, DateTime StartTime, DateTime EndTime, string Status);
}
