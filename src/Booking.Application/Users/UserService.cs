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
}
