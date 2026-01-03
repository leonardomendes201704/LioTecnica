using Microsoft.AspNetCore.Mvc;
using RhPortal.Api.Application.Managers.Handlers;
using RhPortal.Api.Contracts.Common;
using RhPortal.Api.Contracts.Managers;

namespace RhPortal.Api.Controllers;

[ApiController]
[Route("api/managers")]
public sealed class ManagersController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<PagedResult<ManagerGridRowResponse>>> List(
        [FromQuery] ManagerListQuery query,
        [FromServices] IListManagersHandler handler,
        CancellationToken ct)
        => Ok(await handler.HandleAsync(query, ct));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ManagerResponse>> GetById(
        [FromRoute] Guid id,
        [FromServices] IGetManagerByIdHandler handler,
        CancellationToken ct)
    {
        var item = await handler.HandleAsync(id, ct);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<ManagerResponse>> Create(
        [FromBody] ManagerCreateRequest request,
        [FromServices] ICreateManagerHandler handler,
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
    public async Task<ActionResult<ManagerResponse>> Update(
        [FromRoute] Guid id,
        [FromBody] ManagerUpdateRequest request,
        [FromServices] IUpdateManagerHandler handler,
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
        [FromServices] IDeleteManagerHandler handler,
        CancellationToken ct)
    {
        var deleted = await handler.HandleAsync(id, ct);
        return deleted ? NoContent() : NotFound();
    }
}
