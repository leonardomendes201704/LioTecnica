using LioTecnica.Web.Infrastructure.ApiClients;
using LioTecnica.Web.Infrastructure.Security;
using LioTecnica.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace LioTecnica.Web.Controllers;

[ApiController]
[Route("api/lookup")]
public sealed class LookupController : ControllerBase
{
    private readonly AreasApiClient _areas;
    private readonly DepartmentsApiClient _departments;
    private readonly VagasApiClient _vagas;
    private readonly IGestoresLookupService _gestores;
    private readonly JobPositionsApiClient _jobPositions;
    private readonly PortalTenantContext _tenantContext;

    public LookupController(
        AreasApiClient areas,
        DepartmentsApiClient departments,
        VagasApiClient vagas,
        IGestoresLookupService gestores,
        JobPositionsApiClient jobPositions,
        PortalTenantContext tenantContext)
    {
        _areas = areas;
        _departments = departments;
        _vagas = vagas;
        _gestores = gestores;
        _jobPositions = jobPositions;
        _tenantContext = tenantContext;
    }

    [HttpGet("areas")]
    public async Task<IActionResult> Areas(CancellationToken ct)
    {
        var areas = await _areas.GetAreasAsync(_tenantContext.TenantId, ct);

        var result = areas
            .Where(a => a.IsActive)
            .Select(a => new { id = a.Id, code = a.Code, name = a.Name })
            .ToList();

        return Ok(result);
    }

    [HttpGet("departments")]
    public async Task<IActionResult> Departments(CancellationToken ct)
    {
        var list = await _departments.GetLookupOptionsAsync(_tenantContext.TenantId, ct);
        return Ok(list);
    }

    [HttpGet("enums")]
    public async Task<IActionResult> Enums(CancellationToken ct)
    {
        var resp = await _vagas.GetEnumsRawAsync(_tenantContext.TenantId, ct);
        if (string.IsNullOrWhiteSpace(resp.Content))
            return new StatusCodeResult((int)resp.StatusCode);

        return new ContentResult
        {
            StatusCode = (int)resp.StatusCode,
            ContentType = "application/json",
            Content = resp.Content
        };
    }

    [HttpGet("job-positions")]
    public async Task<IActionResult> JobPositions([FromQuery] Guid? areaId, CancellationToken ct)
    {
        var list = await _jobPositions.GetLookupOptionsAsync(_tenantContext.TenantId, areaId, ct);
        var items = list
            .Select(x => new { id = x.Id, code = x.Code, name = x.Name })
            .ToList();

        return Ok(items);
    }

    // GET /api/lookup/managers?onlyActive=true&page=1&pageSize=50&q=ana
    [HttpGet("managers")]
    public async Task<ActionResult<LookupResponse<GestorLookupItem>>> Managers(
        [FromQuery] string? q = null,
        [FromQuery] bool onlyActive = true,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 50,
        CancellationToken ct = default)
    {
        page = Math.Max(1, page);
        pageSize = Math.Clamp(pageSize, 5, 200);

        var resp = await _gestores.LookupAsync(q, onlyActive, page, pageSize, ct);
        return Ok(resp);
    }

}
