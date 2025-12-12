using Booking.Application.Users;
using Booking.Application.Users.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Booking.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    // GET api/users/{id}
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<UserDto>> GetById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var user = await _userService.GetByIdAsync(id, cancellationToken);
        return Ok(user);
    }

    // GET api/users/by-email?email=...
    [HttpGet("by-email")]
    public async Task<ActionResult<UserDto>> GetByEmail(
        [FromQuery] string email,
        CancellationToken cancellationToken)
    {
        var user = await _userService.GetByEmailAsync(email, cancellationToken);
        return Ok(user);
    }
}
