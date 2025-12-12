using Booking.Application.Users.Dtos;
using Booking.Domain.Repositories;

namespace Booking.Application.Users;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetByIdAsync(id, cancellationToken);

        return new UserDto(
            user.Id,
            user.Name,
            user.Email
        );
    }

    public async Task<UserDto> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetByEmailAsync(email, cancellationToken);

        return new UserDto(
            user.Id,
            user.Name,
            user.Email
        );
    }
}
