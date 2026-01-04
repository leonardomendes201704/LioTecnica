using LioTecnica.Web.Infrastructure.ApiClients;
using LioTecnica.Web.Infrastructure.Security;
using LioTecnica.Web.ViewModels.Admin;
using Microsoft.AspNetCore.Mvc;

namespace LioTecnica.Web.Controllers;

public sealed class AdminUsersController : Controller
{
    private readonly UsersApiClient _usersApi;
    private readonly RolesApiClient _rolesApi;

    public AdminUsersController(UsersApiClient usersApi, RolesApiClient rolesApi)
    {
        _usersApi = usersApi;
        _rolesApi = rolesApi;
    }

    [RequirePermission("users.read")]
    [HttpGet("/Admin/Users")]
    public async Task<IActionResult> Index(CancellationToken ct)
    {
        var users = await _usersApi.ListAsync(ct);
        var roles = await _rolesApi.ListAsync(ct);

        return View(new UsersPageViewModel
        {
            Users = users,
            Roles = roles
        });
    }

    [RequirePermission("users.write")]
    [HttpGet("/Admin/Users/New")]
    public async Task<IActionResult> New(CancellationToken ct)
    {
        var roles = await _rolesApi.ListAsync(ct);
        return View("Edit", new UserEditViewModel
        {
            IsNew = true,
            Roles = roles
        });
    }

    [RequirePermission("users.write")]
    [HttpPost("/Admin/Users/New")]
    public async Task<IActionResult> Create([FromForm] UserFormModel model, CancellationToken ct)
    {
        if (!ModelState.IsValid || string.IsNullOrWhiteSpace(model.Password))
        {
            var roles = await _rolesApi.ListAsync(ct);
            ModelState.AddModelError(nameof(model.Password), "Password is required.");
            return View("Edit", new UserEditViewModel
            {
                IsNew = true,
                User = model,
                Roles = roles
            });
        }

        var request = new UsersApiClient.UserCreateRequest(
            model.Email.Trim(),
            model.FullName.Trim(),
            model.Password,
            model.IsActive,
            model.RoleIds
        );

        var created = await _usersApi.CreateAsync(request, ct);
        if (created is null)
        {
            ModelState.AddModelError(string.Empty, "Unable to create user.");
            var roles = await _rolesApi.ListAsync(ct);
            return View("Edit", new UserEditViewModel
            {
                IsNew = true,
                User = model,
                Roles = roles
            });
        }

        return RedirectToAction(nameof(Index));
    }

    [RequirePermission("users.write")]
    [HttpGet("/Admin/Users/Edit/{id:guid}")]
    public async Task<IActionResult> Edit([FromRoute] Guid id, CancellationToken ct)
    {
        var user = await _usersApi.GetByIdAsync(id, ct);
        if (user is null) return NotFound();

        var roles = await _rolesApi.ListAsync(ct);
        var form = new UserFormModel
        {
            Id = user.Id,
            Email = user.Email,
            FullName = user.FullName,
            IsActive = user.IsActive,
            RoleIds = user.Roles.Select(r => r.Id).ToList()
        };

        return View(new UserEditViewModel
        {
            IsNew = false,
            User = form,
            Roles = roles
        });
    }

    [RequirePermission("users.write")]
    [HttpPost("/Admin/Users/Edit/{id:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromForm] UserFormModel model, CancellationToken ct)
    {
        if (!ModelState.IsValid)
        {
            var roles = await _rolesApi.ListAsync(ct);
            return View("Edit", new UserEditViewModel
            {
                IsNew = false,
                User = model,
                Roles = roles
            });
        }

        var updateRequest = new UsersApiClient.UserUpdateRequest(
            model.Email.Trim(),
            model.FullName.Trim(),
            model.IsActive
        );

        var updated = await _usersApi.UpdateAsync(id, updateRequest, ct);
        if (updated is null)
        {
            ModelState.AddModelError(string.Empty, "Unable to update user.");
            var roles = await _rolesApi.ListAsync(ct);
            return View("Edit", new UserEditViewModel
            {
                IsNew = false,
                User = model,
                Roles = roles
            });
        }

        await _usersApi.UpdateRolesAsync(id, model.RoleIds, ct);

        return RedirectToAction(nameof(Index));
    }
}
