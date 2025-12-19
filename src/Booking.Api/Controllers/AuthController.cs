using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Booking.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public AuthController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpPost("login")]
public async Task<IActionResult> Login(
    [FromBody] LoginRequest request,
    [FromServices] IUserService userService,
    CancellationToken cancellationToken)
{
    var user = await userService.GetByEmailAsync(request.Email, cancellationToken);
    if (user is null)
        return Unauthorized("Invalid credentials");

    // Validar password
    var userEntity = await userService.GetEntityByEmailAsync(request.Email, cancellationToken); // nuevo método en IUserService
    if (!BCrypt.Net.BCrypt.Verify(request.Password, userEntity.PasswordHash))
        return Unauthorized("Invalid credentials");

    var token = GenerateJwtToken(user.Email);
    return Ok(new { token });
}


    private string GenerateJwtToken(string email)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, email),
            new Claim(ClaimTypes.Email, email)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

public class LoginRequest
{
    public string Email { get; set; } = string.Empty;
}
