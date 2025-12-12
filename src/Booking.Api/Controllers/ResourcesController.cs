using Booking.Application.Resources;
using Booking.Application.Resources.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;


namespace Booking.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ResourcesController : ControllerBase
{
    private readonly IResourceService _resourceService;

    public ResourcesController(IResourceService resourceService)
    {
        _resourceService = resourceService;
    }

    // GET api/resources
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ResourceDto>>> GetAll(
        CancellationToken cancellationToken)
    {
        var resources = await _resourceService.GetAllAsync(cancellationToken);
        return Ok(resources);
    }

    // GET api/resources/{id}
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ResourceDto>> GetById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var resource = await _resourceService.GetByIdAsync(id, cancellationToken);
        return Ok(resource);
    }
}
