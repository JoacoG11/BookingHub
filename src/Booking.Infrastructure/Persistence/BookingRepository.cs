using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Booking.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using BookingEntity = Booking.Domain.Entities.Booking;

namespace Booking.Infrastructure.Persistence;

public class BookingRepository : IBookingRepository
{
    private readonly AppDbContext _context;

    public BookingRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<BookingEntity?> GetByIdAsync(Guid id)
    {
        return await _context.Bookings
            .Include(b => b.User)
            .Include(b => b.Resource)
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<IReadOnlyList<BookingEntity>> GetByUserAsync(Guid userId)
    {
        return await _context.Bookings
            .Where(b => b.UserId == userId)
            .ToListAsync();
    }

    public async Task<IReadOnlyList<BookingEntity>> GetByResourceAndRangeAsync(Guid resourceId, DateTime from, DateTime to)
    {
        return await _context.Bookings
            .Where(b => b.ResourceId == resourceId &&
                        b.StartTime < to &&
                        b.EndTime > from)
            .ToListAsync();
    }

    public async Task AddAsync(BookingEntity booking)
    {
        await _context.Bookings.AddAsync(booking);
    }

    public Task UpdateAsync(BookingEntity booking)
    {
        _context.Bookings.Update(booking);
        return Task.CompletedTask;
    }
}
