using Booking.Application.Users.Dtos;
using Booking.Domain.Repositories;

namespace Booking.Application.Users;

public class UserService : IUserService
{
    private readonly IUserRepository _users;

    public UserService(IUserRepository users)
    {
        _users = users;
    }

    public async Task<UserDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var user = await _users.GetByIdAsync(id, cancellationToken);

        if (user is null)
            throw new KeyNotFoundException($"User {id} not found");

        return new UserDto(user.Id, user.Name, user.Email);
    }

    public async Task<UserDto> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        var user = await _users.GetByEmailAsync(email, cancellationToken);

        if (user is null)
            throw new KeyNotFoundException($"User with email '{email}' not found");

        return new UserDto(user.Id, user.Name, user.Email);
    }

    public async Task RegisterAsync(RegisterUserRequest request, CancellationToken cancellationToken)
{
    var existingUser = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);
    if (existingUser != null)
        throw new Exception("Email already registered");

    var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

    var newUser = new User
    {
        Id = Guid.NewGuid(),
        Email = request.Email,
        PasswordHash = hashedPassword,
        Role = "User",
        CreatedAt = DateTime.UtcNow
    };

    await _userRepository.CreateAsync(newUser, cancellationToken);
}

public async Task<User> GetEntityByEmailAsync(string email, CancellationToken cancellationToken)
{
    var user = await _userRepository.GetByEmailAsync(email, cancellationToken);
    if (user is null)
        throw new Exception("User not found");

    return user;
}


}
