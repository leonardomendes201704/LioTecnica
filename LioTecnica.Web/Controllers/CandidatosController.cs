using System.Text.Json;
using LioTecnica.Web.Infrastructure.ApiClients;
using LioTecnica.Web.Infrastructure.Security;
using LioTecnica.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LioTecnica.Web.Controllers;

public class CandidatosController : Controller
{
    private readonly CandidatosApiClient _candidatosApi;
    private readonly PortalTenantContext _tenantContext;

    public CandidatosController(CandidatosApiClient candidatosApi, PortalTenantContext tenantContext)
    {
        _candidatosApi = candidatosApi;
        _tenantContext = tenantContext;
    }

    public IActionResult Index()
    {
        var model = new PageSeedViewModel { SeedJson = "{}" };
        return View(model);
    }

    [HttpGet("/api/candidatos")]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var tenantId = _tenantContext.TenantId;
        var resp = await _candidatosApi.GetCandidatosRawAsync(tenantId, ct);
        return ToContentResult(resp);
    }

    [HttpGet("/api/candidatos/{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
    {
        var tenantId = _tenantContext.TenantId;
        var resp = await _candidatosApi.GetCandidatoByIdRawAsync(tenantId, id, ct);
        return ToContentResult(resp);
    }

    [HttpPost("/api/candidatos")]
    public async Task<IActionResult> Create([FromBody] JsonElement payload, CancellationToken ct)
    {
        var tenantId = _tenantContext.TenantId;
        var resp = await _candidatosApi.CreateRawAsync(tenantId, payload, ct);
        return ToContentResult(resp);
    }

    [HttpPut("/api/candidatos/{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] JsonElement payload, CancellationToken ct)
    {
        var tenantId = _tenantContext.TenantId;
        var resp = await _candidatosApi.UpdateRawAsync(tenantId, id, payload, ct);
        return ToContentResult(resp);
    }

    [HttpDelete("/api/candidatos/{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var tenantId = _tenantContext.TenantId;
        var resp = await _candidatosApi.DeleteRawAsync(tenantId, id, ct);
        return ToContentResult(resp);
    }

    private static IActionResult ToContentResult(ApiRawResponse resp)
    {
        if (string.IsNullOrWhiteSpace(resp.Content))
            return new StatusCodeResult((int)resp.StatusCode);

        return new ContentResult
        {
            StatusCode = (int)resp.StatusCode,
            ContentType = "application/json",
            Content = resp.Content
        };
    }
}
