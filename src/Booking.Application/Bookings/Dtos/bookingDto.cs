namespace Booking.Application.Bookings.Dtos;

public record BookingDto(
    Guid Id,
    Guid UserId,
    Guid ResourceId,
    DateTime StartTime,
    DateTime EndTime,
    string Status
);
