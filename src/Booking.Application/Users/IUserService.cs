using Booking.Application.Users.Dtos;

namespace Booking.Application.Users;

public interface IUserService
{
    Task<UserDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<UserDto> GetByEmailAsync(string email, CancellationToken cancellationToken = default);

    Task RegisterAsync(RegisterUserRequest request, CancellationToken cancellationToken);

    Task<User> GetEntityByEmailAsync(string email, CancellationToken cancellationToken);


}
