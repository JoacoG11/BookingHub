using Booking.Application.Resources.Dtos;
using Booking.Domain.Repositories;

namespace Booking.Application.Resources;

public class ResourceService : IResourceService
{
    private readonly IResourceRepository _resources;

    public ResourceService(IResourceRepository resources)
    {
        _resources = resources;
    }

    public async Task<IReadOnlyList<ResourceDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var list = await _resources.GetAllAsync(cancellationToken);

        return list
            .Select(r => new ResourceDto(r.Id, r.Name, r.Description, r.Capacity, r.IsActive))
            .ToList()
            .AsReadOnly();
    }

    public async Task<ResourceDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var resource = await _resources.GetByIdAsync(id, cancellationToken);

        if (resource is null)
            throw new KeyNotFoundException($"Resource {id} not found");

        return new ResourceDto(resource.Id, resource.Name, resource.Description, resource.Capacity, resource.IsActive);
    }
}
