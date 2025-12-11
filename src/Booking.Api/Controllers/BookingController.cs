using Booking.Application.Bookings;
using Booking.Application.Bookings.Commands;
using Booking.Application.Bookings.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookingsController : ControllerBase
{
    private readonly IBookingService _bookingService;

    public BookingsController(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    // POST api/bookings
    [HttpPost]
    public async Task<ActionResult<BookingDto>> Create(
        [FromBody] CreateBookingRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _bookingService.CreateAsync(request, cancellationToken);

        // Devolvemos 201 Created con el recurso creado
        return CreatedAtAction(
            nameof(GetByUser),
            new { userId = result.UserId },
            result
        );
    }

    // GET api/bookings/user/{userId}
    [HttpGet("user/{userId:guid}")]
    public async Task<ActionResult<IReadOnlyList<BookingDto>>> GetByUser(
        Guid userId,
        CancellationToken cancellationToken)
    {
        var bookings = await _bookingService.GetByUserAsync(userId, cancellationToken);
        return Ok(bookings);
    }
}
