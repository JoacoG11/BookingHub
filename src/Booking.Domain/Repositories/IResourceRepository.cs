using Booking.Domain.Entities;

namespace Booking.Domain.Repositories
{
    public interface IResourceRepository
    {
        Task<Resource?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Resource>> GetAllAsync(CancellationToken cancellationToken = default);
        Task AddAsync(Resource resource, CancellationToken cancellationToken = default);
    }
}
