using Booking.Domain.Entities;
using Booking.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

// alias claro para la entidad Booking
using BookingEntity = Booking.Domain.Entities.Booking;

namespace Booking.Infrastructure.Persistence;

public class BookingRepository : IBookingRepository
{
    private readonly AppDbContext _context;

    public BookingRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<BookingEntity> GetByIdAsync(Guid id)
    {
        return await _context.Bookings
            .AsNoTracking()
            .FirstOrDefaultAsync(b => b.Id == id)
            ?? throw new KeyNotFoundException($"Booking {id} not found");
    }

    public async Task<IReadOnlyList<BookingEntity>> GetByUserAsync(Guid userId)
    {
        return await _context.Bookings
            .AsNoTracking()
            .Where(b => b.UserId == userId)   // 👈 importante: solo filtramos por UserId
            .OrderByDescending(b => b.StartTime)
            .ToListAsync();
    }

    public async Task<IReadOnlyList<BookingEntity>> GetByResourceAndRangeAsync(
        Guid resourceId,
        DateTime from,
        DateTime to)
    {
        return await _context.Bookings
            .AsNoTracking()
            .Where(b =>
                b.ResourceId == resourceId &&
                b.StartTime < to &&
                b.EndTime   > from)
            .ToListAsync();
    }

    public async Task AddAsync(BookingEntity booking)
    {
        _context.Bookings.Add(booking);
        await _context.SaveChangesAsync();   // 👈 aseguramos el INSERT
    }

    public async Task UpdateAsync(BookingEntity booking)
    {
        _context.Bookings.Update(booking);
        await _context.SaveChangesAsync();   // 👈 aseguramos el UPDATE
    }
}
