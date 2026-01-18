using LioTecnica.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using LioTecnica.Web.Infrastructure.Security;
using RhPortal.Web.Infrastructure.ApiClients;

namespace LioTecnica.Web.Controllers;

public class GestoresController : Controller
{
    private readonly ManagersApiClient _managersApi;
    private readonly PortalTenantContext _tenantContext;

    public GestoresController(ManagersApiClient managersApi, PortalTenantContext tenantContext)
    {
        _managersApi = managersApi;
        _tenantContext = tenantContext;
    }

    [HttpGet("/Gestores")]
    public IActionResult Index()
    {
        var model = new PageSeedViewModel
        {
            SeedJson = "{}"
        };

        return View(model);
    }

    [HttpGet("/Gestores/_api")]
    public async Task<IActionResult> List(
        [FromQuery] string? search,
        [FromQuery] string? status,
        [FromQuery] Guid? unitId,
        [FromQuery] Guid? areaId,
        [FromQuery] Guid? jobPositionId,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 200,
        CancellationToken ct = default)
    {
        var tenantId = _tenantContext.TenantId;

        var apiStatus = status?.ToLowerInvariant() switch
        {
            "ativo" => "Active",
            "inativo" => "Inactive",
            _ => null
        };

        var api = await _managersApi.GetManagersAsync(
            tenantId, search, apiStatus, unitId, areaId, jobPositionId, page, pageSize, sort: "manager", dir: "asc", ct);

        var items = (api.Items ?? new()).Select(MapToFrontGestor).ToList();

        return Ok(new
        {
            items,
            api.Page,
            api.PageSize,
            api.TotalItems,
            api.TotalPages
        });
    }

    [HttpGet("/Gestores/_api/{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken ct)
    {
        var tenantId = _tenantContext.TenantId;
        var item = await _managersApi.GetByIdAsync(tenantId, id, ct);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost("/Gestores/_api")]
    public async Task<IActionResult> Create([FromBody] ManagerCreateRequest request, CancellationToken ct)
    {
        var tenantId = _tenantContext.TenantId;
        var created = await _managersApi.CreateAsync(tenantId, request, ct);
        return Ok(created);
    }

    [HttpPut("/Gestores/_api/{id:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] ManagerUpdateRequest request, CancellationToken ct)
    {
        var tenantId = _tenantContext.TenantId;
        var updated = await _managersApi.UpdateAsync(tenantId, id, request, ct);
        return updated is null ? NotFound() : Ok(updated);
    }

    [HttpDelete("/Gestores/_api/{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken ct)
    {
        var tenantId = _tenantContext.TenantId;
        var ok = await _managersApi.DeleteAsync(tenantId, id, ct);
        return ok ? NoContent() : NotFound();
    }

    private static object MapToFrontGestor(ManagerApiItem m)
        => new
        {
            id = m.Id.ToString(),
            nome = m.Name ?? "",
            email = m.Email ?? "",
            telefone = m.Phone ?? "",
            status = (m.Status?.Equals("Active", StringComparison.OrdinalIgnoreCase) ?? false) ? "ativo" : "inativo",
            headcount = m.Headcount,
            unidade = m.UnitName ?? "",
            unidadeId = m.UnitId,
            area = m.AreaName ?? "",
            areaId = m.AreaId,
            cargo = m.JobPositionName ?? "",
            cargoId = m.JobPositionId
        };
}
