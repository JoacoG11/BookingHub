using Booking.Application.Resources.Dtos;

namespace Booking.Application.Resources;

public interface IResourceService
{
    Task<IReadOnlyList<ResourceDto>> GetAllAsync(CancellationToken cancellationToken = default);
    
    Task<ResourceDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}
