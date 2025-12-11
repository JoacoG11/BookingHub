using Booking.Application.Bookings.Commands;
using Booking.Application.Bookings.Dtos;

namespace Booking.Application.Bookings;

public interface IBookingService
{
    Task<BookingDto> CreateAsync(CreateBookingRequest request, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<BookingDto>> GetByUserAsync(Guid userId, CancellationToken cancellationToken = default);
}
