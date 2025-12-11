using Booking.Domain.Entities;

namespace Booking.Domain.Repositories;

using BookingEntity = Booking.Domain.Entities.Booking;

public interface IBookingRepository
{
    Task<BookingEntity?> GetByIdAsync(Guid id);   // ← Ahora permite null

    Task<IReadOnlyList<BookingEntity>> GetByUserAsync(Guid userId);

    Task<IReadOnlyList<BookingEntity>> GetByResourceAndRangeAsync(
        Guid resourceId,
        DateTime from,
        DateTime to
    );

    Task AddAsync(BookingEntity booking);

    Task UpdateAsync(BookingEntity booking);
}
