namespace Booking.Application.Users.Dtos;

public record UserDto(
    Guid Id,
    string Name,
    string Email
);
