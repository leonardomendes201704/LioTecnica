using Microsoft.AspNetCore.Mvc;
using RhPortal.Api.Application.Candidatos.Handlers;
using RhPortal.Api.Contracts.Candidatos;
using RhPortal.Api.Domain.Enums;

namespace RhPortal.Api.Controllers;

[ApiController]
[Route("api/candidatos")]
public sealed class CandidatosController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<CandidatoListItemResponse>>> List(
        [FromQuery] string? q,
        [FromQuery] CandidatoStatus? status,
        [FromQuery] Guid? vagaId,
        [FromQuery] CandidatoFonte? fonte,
        [FromServices] IListCandidatosHandler handler,
        CancellationToken ct)
    {
        var query = new CandidatoListQuery(q, status, vagaId, fonte);
        var items = await handler.HandleAsync(query, ct);
        return Ok(items);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CandidatoResponse>> GetById(
        [FromRoute] Guid id,
        [FromServices] IGetCandidatoByIdHandler handler,
        CancellationToken ct)
    {
        var item = await handler.HandleAsync(id, ct);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<CandidatoResponse>> Create(
        [FromBody] CandidatoCreateRequest request,
        [FromServices] ICreateCandidatoHandler handler,
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
    public async Task<ActionResult<CandidatoResponse>> Update(
        [FromRoute] Guid id,
        [FromBody] CandidatoUpdateRequest request,
        [FromServices] IUpdateCandidatoHandler handler,
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
        [FromServices] IDeleteCandidatoHandler handler,
        CancellationToken ct)
    {
        var deleted = await handler.HandleAsync(id, ct);
        return deleted ? NoContent() : NotFound();
    }
}
