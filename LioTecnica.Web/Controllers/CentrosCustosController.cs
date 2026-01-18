using LioTecnica.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using LioTecnica.Web.Infrastructure.Security;
using LioTecnica.Web.Infrastructure.ApiClients;

namespace LioTecnica.Web.Controllers;

public class CentrosCustosController : Controller
{
    private readonly CostCentersApiClient _costCentersApi;
    private readonly DepartmentsApiClient _departmentsApi;
    private readonly PortalTenantContext _tenantContext;

    public CentrosCustosController(
        CostCentersApiClient costCentersApi,
        DepartmentsApiClient departmentsApi,
        PortalTenantContext tenantContext)
    {
        _costCentersApi = costCentersApi;
        _departmentsApi = departmentsApi;
        _tenantContext = tenantContext;
    }

    [HttpGet("/CentrosCustos")]
    public IActionResult Index()
    {
        var model = new PageSeedViewModel
        {
            SeedJson = "{}"
        };
        return View(model);
    }

    [HttpGet("/CentrosCustos/_api")]
    public async Task<IActionResult> List(CancellationToken ct)
    {
        var tenantId = _tenantContext.TenantId;
        var items = await _costCentersApi.GetCostCentersAsync(tenantId, ct);
        return Ok(new { items });
    }

    [HttpGet("/CentrosCustos/_api/{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken ct)
    {
        var tenantId = _tenantContext.TenantId;
        var item = await _costCentersApi.GetByIdAsync(tenantId, id, ct);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost("/CentrosCustos/_api")]
    public async Task<IActionResult> Create([FromBody] CostCenterCreateRequest request, CancellationToken ct)
    {
        var tenantId = _tenantContext.TenantId;
        var created = await _costCentersApi.CreateAsync(tenantId, request, ct);
        return Ok(created);
    }

    [HttpPut("/CentrosCustos/_api/{id:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] CostCenterUpdateRequest request, CancellationToken ct)
    {
        var tenantId = _tenantContext.TenantId;
        var updated = await _costCentersApi.UpdateAsync(tenantId, id, request, ct);
        return updated is null ? NotFound() : Ok(updated);
    }

    [HttpDelete("/CentrosCustos/_api/{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken ct)
    {
        var tenantId = _tenantContext.TenantId;
        var ok = await _costCentersApi.DeleteAsync(tenantId, id, ct);
        return ok ? NoContent() : NotFound();
    }

    [HttpGet("/CentrosCustos/_api/departments")]
    public async Task<IActionResult> Departments([FromQuery] int page = 1, [FromQuery] int pageSize = 200, CancellationToken ct = default)
    {
        var tenantId = _tenantContext.TenantId;

        var api = await _departmentsApi.GetDepartmentsAsync(
            tenantId, search: null, status: null, areaId: null, page: page, pageSize: pageSize, sort: "department", dir: "asc", ct);

        return Ok(new
        {
            items = api.Items,
            api.Page,
            api.PageSize,
            api.TotalItems,
            api.TotalPages
        });
    }
}
