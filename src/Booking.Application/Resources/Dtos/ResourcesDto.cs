namespace Booking.Application.Resources.Dtos;

public record ResourceDto(
    Guid Id,
    string Name,
    string? Description,
    int Capacity,
    bool IsActive
);
