using System.Security.Claims;
using LioTecnica.Web.Infrastructure.ApiClients;
using LioTecnica.Web.ViewModels.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LioTecnica.Web.Controllers;

public sealed class AccountController : Controller
{
    private readonly AuthApiClient _authApi;

    public AccountController(AuthApiClient authApi)
    {
        _authApi = authApi;
    }

    [AllowAnonymous]
    [HttpGet("/Account/Login")]
    public IActionResult Login([FromQuery] string? returnUrl = null)
    {
        return View(new LoginViewModel { ReturnUrl = returnUrl });
    }

    [AllowAnonymous]
    [HttpPost("/Account/Login")]
    public async Task<IActionResult> Login([FromForm] LoginViewModel model, CancellationToken ct)
    {
        if (!ModelState.IsValid)
            return View(model);

        var response = await _authApi.LoginAsync(model.TenantId.Trim(), model.Email.Trim(), model.Password, ct);
        if (response is null)
        {
            ModelState.AddModelError(string.Empty, "Invalid credentials.");
            return View(model);
        }

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, response.UserId.ToString()),
            new(ClaimTypes.Email, response.Email),
            new(ClaimTypes.Name, response.FullName),
            new("tenant", response.TenantId),
            new("access_token", response.AccessToken)
        };

        foreach (var role in response.Roles ?? Array.Empty<string>())
            claims.Add(new Claim(ClaimTypes.Role, role));

        foreach (var permission in response.Permissions ?? Array.Empty<string>())
            claims.Add(new Claim("permission", permission));

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            principal,
            new AuthenticationProperties { IsPersistent = false });

        var redirectUrl = string.IsNullOrWhiteSpace(model.ReturnUrl) ? "/" : model.ReturnUrl;
        return LocalRedirect(redirectUrl);
    }

    [HttpPost("/Account/Logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction(nameof(Login));
    }
}
