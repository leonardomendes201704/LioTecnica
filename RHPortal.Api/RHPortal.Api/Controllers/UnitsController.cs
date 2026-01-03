using Microsoft.AspNetCore.Mvc;
using RhPortal.Api.Application.Units.Handlers;
using RhPortal.Api.Contracts.Common;
using RhPortal.Api.Contracts.Units;

namespace RhPortal.Api.Controllers;

[ApiController]
[Route("api/units")]
[Produces("application/json")]
public sealed class UnitsController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<PagedResult<UnitGridRowResponse>>> List(
        [FromQuery] UnitListQuery query,
        [FromServices] IListUnitsHandler handler,
        CancellationToken ct)
        => Ok(await handler.HandleAsync(query, ct));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<UnitResponse>> GetById(
        [FromRoute] Guid id,
        [FromServices] IGetUnitByIdHandler handler,
        CancellationToken ct)
    {
        var item = await handler.HandleAsync(id, ct);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<UnitResponse>> Create(
        [FromBody] UnitCreateRequest request,
        [FromServices] ICreateUnitHandler handler,
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
    public async Task<ActionResult<UnitResponse>> Update(
        [FromRoute] Guid id,
        [FromBody] UnitUpdateRequest request,
        [FromServices] IUpdateUnitHandler handler,
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
        [FromServices] IDeleteUnitHandler handler,
        CancellationToken ct)
    {
        var deleted = await handler.HandleAsync(id, ct);
        return deleted ? NoContent() : NotFound();
    }
}
