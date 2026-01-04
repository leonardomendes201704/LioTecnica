using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using RhPortal.Api.Application.Menus;
using RhPortal.Api.Contracts.Menus;
using RhPortal.Api.Infrastructure.Security;

namespace RhPortal.Api.Controllers;

[ApiController]
[Route("api/menus")]
public sealed class MenusController : ControllerBase
{
    [RequirePermission("menus.manage")]
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<MenuListItemResponse>>> List(
        [FromServices] MenuAdministrationService service,
        CancellationToken ct)
    {
        var items = await service.ListAsync(ct);
        return Ok(items);
    }

    [RequirePermission("menus.manage")]
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<MenuResponse>> GetById(
        [FromRoute] Guid id,
        [FromServices] MenuAdministrationService service,
        CancellationToken ct)
    {
        var item = await service.GetByIdAsync(id, ct);
        return item is null ? NotFound() : Ok(item);
    }

    [RequirePermission("menus.manage")]
    [HttpPost]
    public async Task<ActionResult<MenuResponse>> Create(
        [FromBody] MenuCreateRequest request,
        [FromServices] MenuAdministrationService service,
        CancellationToken ct)
    {
        try
        {
            var created = await service.CreateAsync(request, ct);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new ProblemDetails
            {
                Title = "Unable to create menu.",
                Detail = ex.Message,
                Status = StatusCodes.Status409Conflict
            });
        }
    }

    [RequirePermission("menus.manage")]
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<MenuResponse>> Update(
        [FromRoute] Guid id,
        [FromBody] MenuUpdateRequest request,
        [FromServices] MenuAdministrationService service,
        CancellationToken ct)
    {
        try
        {
            var updated = await service.UpdateAsync(id, request, ct);
            return updated is null ? NotFound() : Ok(updated);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new ProblemDetails
            {
                Title = "Unable to update menu.",
                Detail = ex.Message,
                Status = StatusCodes.Status409Conflict
            });
        }
    }

    [RequirePermission("menus.manage")]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(
        [FromRoute] Guid id,
        [FromServices] MenuAdministrationService service,
        CancellationToken ct)
    {
        var deleted = await service.DeleteAsync(id, ct);
        return deleted ? NoContent() : NotFound();
    }

    [HttpGet("for-current-user")]
    public async Task<ActionResult<IReadOnlyList<MenuForCurrentUserResponse>>> ForCurrentUser(
        [FromServices] MenuAdministrationService service,
        CancellationToken ct)
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!Guid.TryParse(userIdClaim, out var userId))
        {
            return Unauthorized(new ProblemDetails
            {
                Title = "Invalid token.",
                Detail = "User identifier is missing.",
                Status = StatusCodes.Status401Unauthorized
            });
        }

        var items = await service.ListForUserAsync(userId, ct);
        return Ok(items);
    }
}
