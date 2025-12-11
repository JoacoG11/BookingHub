namespace Booking.Application.Bookings.Commands;

public record CreateBookingRequest(
    Guid UserId,
    Guid ResourceId,
    DateTime StartTime,
    DateTime EndTime
);
