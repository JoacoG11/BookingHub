using Booking.Application.Resources.Dtos;
using Booking.Domain.Repositories;

namespace Booking.Application.Resources;

public class ResourceService : IResourceService
{
    private readonly IResourceRepository _resourceRepository;

    public ResourceService(IResourceRepository resourceRepository)
    {
        _resourceRepository = resourceRepository;
    }

    public async Task<IReadOnlyList<ResourceDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var resources = await _resourceRepository.GetAllAsync(cancellationToken);

        return resources
            .Select(r => new ResourceDto(
                r.Id,
                r.Name,
                r.Description,
                r.Capacity,
                r.IsActive
            ))
            .ToList()
            .AsReadOnly();
    }

    public async Task<ResourceDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var resource = await _resourceRepository.GetByIdAsync(id, cancellationToken);

        return new ResourceDto(
            resource.Id,
            resource.Name,
            resource.Description,
            resource.Capacity,
            resource.IsActive
        );
    }
}
