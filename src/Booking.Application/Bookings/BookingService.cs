using Booking.Application.Bookings.Commands;
using Booking.Application.Bookings.Dtos;
using Booking.Domain.Entities;
using Booking.Domain.Repositories;

// alias claro para la entidad Booking
using BookingEntity = Booking.Domain.Entities.Booking;

namespace Booking.Application.Bookings;

public class BookingService : IBookingService
{
    private readonly IBookingRepository _bookingRepository;

    public BookingService(IBookingRepository bookingRepository)
    {
        _bookingRepository = bookingRepository;
    }

    public async Task<BookingDto> CreateAsync(
        CreateBookingRequest request,
        CancellationToken cancellationToken = default)
    {
        // Regla básica: la hora de inicio debe ser menor que la de fin.
        if (request.StartTime >= request.EndTime)
            throw new ArgumentException("Start time must be before end time.");

        // Regla de negocio sencilla: no permitir solapamiento con reservas existentes
        var existing = await _bookingRepository.GetByResourceAndRangeAsync(
            request.ResourceId,
            request.StartTime,
            request.EndTime
        );

        if (existing.Any(b => b.Status != BookingStatus.Cancelled))
        {
            throw new InvalidOperationException("Resource is already booked in that time range.");
        }

        // Crear la entidad de dominio usando el alias
        var booking = new BookingEntity(
            request.UserId,
            request.ResourceId,
            request.StartTime,
            request.EndTime
        );

        await _bookingRepository.AddAsync(booking);

        return ToDto(booking);
    }

    public async Task<IReadOnlyList<BookingDto>> GetByUserAsync(
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        var bookings = await _bookingRepository.GetByUserAsync(userId);

        return bookings
            .Select(ToDto)
            .ToList()
            .AsReadOnly();
    }

    private static BookingDto ToDto(BookingEntity booking)
        => new(
            booking.Id,
            booking.UserId,
            booking.ResourceId,
            booking.StartTime,
            booking.EndTime,
            booking.Status.ToString()
        );
}
