using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Booking.Domain.Entities;
using Booking.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Booking.Infrastructure.Persistence;

public class ResourceRepository : IResourceRepository
{
    private readonly AppDbContext _context;

    public ResourceRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Resource?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Resources.FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Resource>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Resources
            .Where(r => r.IsActive)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Resource resource, CancellationToken cancellationToken = default)
    {
        await _context.Resources.AddAsync(resource, cancellationToken);
    }
}
