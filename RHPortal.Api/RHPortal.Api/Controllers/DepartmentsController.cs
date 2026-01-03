using Microsoft.AspNetCore.Mvc;
using RhPortal.Api.Application.Departments.Handlers;
using RhPortal.Api.Contracts.Common;
using RhPortal.Api.Contracts.Departments;

namespace RhPortal.Api.Controllers;

[ApiController]
[Route("api/departments")]
public sealed class DepartmentsController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<PagedResult<DepartmentGridRowResponse>>> List(
        [FromQuery] DepartmentListQuery query,
        [FromServices] IListDepartmentsHandler handler,
        CancellationToken ct)
        => Ok(await handler.HandleAsync(query, ct));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<DepartmentResponse>> GetById(
        [FromRoute] Guid id,
        [FromServices] IGetDepartmentByIdHandler handler,
        CancellationToken ct)
    {
        var item = await handler.HandleAsync(id, ct);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<DepartmentResponse>> Create(
        [FromBody] DepartmentCreateRequest request,
        [FromServices] ICreateDepartmentHandler handler,
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
    public async Task<ActionResult<DepartmentResponse>> Update(
        [FromRoute] Guid id,
        [FromBody] DepartmentUpdateRequest request,
        [FromServices] IUpdateDepartmentHandler handler,
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
        [FromServices] IDeleteDepartmentHandler handler,
        CancellationToken ct)
    {
        var deleted = await handler.HandleAsync(id, ct);
        return deleted ? NoContent() : NotFound();
    }
}
