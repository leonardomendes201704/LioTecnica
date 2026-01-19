using Microsoft.AspNetCore.Mvc;
using RhPortal.Api.Application.Users;
using RhPortal.Api.Contracts.Users;
using RhPortal.Api.Infrastructure.Security;

namespace RhPortal.Api.Controllers;

[ApiController]
[Route("api/users")]
public sealed class UsersController : ControllerBase
{
    [RequirePermission("users.read")]
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<UserListItemResponse>>> List(
        [FromServices] UserAdministrationService service,
        CancellationToken ct)
    {
        var items = await service.ListAsync(ct);
        return Ok(items);
    }

    [RequirePermission("users.read")]
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<UserResponse>> GetById(
        [FromRoute] Guid id,
        [FromServices] UserAdministrationService service,
        CancellationToken ct)
    {
        var item = await service.GetByIdAsync(id, ct);
        return item is null ? NotFound() : Ok(item);
    }

    [RequirePermission("users.write")]
    [HttpPost]
    public async Task<ActionResult<UserResponse>> Create(
        [FromBody] UserCreateRequest request,
        [FromServices] UserAdministrationService service,
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
                Title = "Unable to create user.",
                Detail = ex.Message,
                Status = StatusCodes.Status409Conflict
            });
        }
    }

    [RequirePermission("users.write")]
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<UserResponse>> Update(
        [FromRoute] Guid id,
        [FromBody] UserUpdateRequest request,
        [FromServices] UserAdministrationService service,
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
                Title = "Unable to update user.",
                Detail = ex.Message,
                Status = StatusCodes.Status409Conflict
            });
        }
    }

    [RequirePermission("users.write")]
    [HttpPatch("{id:guid}/status")]
    public async Task<ActionResult<UserResponse>> UpdateStatus(
        [FromRoute] Guid id,
        [FromBody] UserStatusUpdateRequest request,
        [FromServices] UserAdministrationService service,
        CancellationToken ct)
    {
        try
        {
            var updated = await service.UpdateStatusAsync(id, request.IsActive, ct);
            return updated is null ? NotFound() : Ok(updated);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new ProblemDetails
            {
                Title = "Unable to update user status.",
                Detail = ex.Message,
                Status = StatusCodes.Status409Conflict
            });
        }
    }

    [RequirePermission("users.write")]
    [HttpPut("{id:guid}/roles")]
    public async Task<ActionResult<UserResponse>> UpdateRoles(
        [FromRoute] Guid id,
        [FromBody] UserRolesUpdateRequest request,
        [FromServices] UserAdministrationService service,
        CancellationToken ct)
    {
        try
        {
            var updated = await service.UpdateRolesAsync(id, request.RoleIds, ct);
            return updated is null ? NotFound() : Ok(updated);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new ProblemDetails
            {
                Title = "Unable to update user roles.",
                Detail = ex.Message,
                Status = StatusCodes.Status409Conflict
            });
        }
    }

    [RequirePermission("users.write")]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(
        [FromRoute] Guid id,
        [FromServices] UserAdministrationService service,
        CancellationToken ct)
    {
        try
        {
            var removed = await service.DeleteAsync(id, ct);
            return removed ? NoContent() : NotFound();
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new ProblemDetails
            {
                Title = "Unable to delete user.",
                Detail = ex.Message,
                Status = StatusCodes.Status409Conflict
            });
        }
    }
}
