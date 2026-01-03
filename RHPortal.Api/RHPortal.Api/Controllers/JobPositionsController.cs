using Microsoft.AspNetCore.Mvc;
using RhPortal.Api.Application.JobPositions.Handlers;
using RhPortal.Api.Contracts.Common;
using RhPortal.Api.Contracts.JobPositions;

namespace RhPortal.Api.Controllers;

[ApiController]
[Route("api/job-positions")]
public sealed class JobPositionsController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<PagedResult<JobPositionGridRowResponse>>> List(
        [FromQuery] JobPositionListQuery query,
        [FromServices] IListJobPositionsHandler handler,
        CancellationToken ct)
        => Ok(await handler.HandleAsync(query, ct));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<JobPositionResponse>> GetById(
        [FromRoute] Guid id,
        [FromServices] IGetJobPositionByIdHandler handler,
        CancellationToken ct)
    {
        var item = await handler.HandleAsync(id, ct);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<JobPositionResponse>> Create(
        [FromBody] JobPositionCreateRequest request,
        [FromServices] ICreateJobPositionHandler handler,
        CancellationToken ct)
    {
        try
        {
            var created = await handler.HandleAsync(request, ct);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<JobPositionResponse>> Update(
        [FromRoute] Guid id,
        [FromBody] JobPositionUpdateRequest request,
        [FromServices] IUpdateJobPositionHandler handler,
        CancellationToken ct)
    {
        try
        {
            var updated = await handler.HandleAsync(id, request, ct);
            return updated is null ? NotFound() : Ok(updated);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(
        [FromRoute] Guid id,
        [FromServices] IDeleteJobPositionHandler handler,
        CancellationToken ct)
    {
        var deleted = await handler.HandleAsync(id, ct);
        return deleted ? NoContent() : NotFound();
    }
}
