using Microsoft.AspNetCore.Mvc;
using LioTecnica.Web.Infrastructure.ApiClients;
using LioTecnica.Web.Services;

namespace LioTecnica.Web.Controllers;

[ApiController]
[Route("api/lookup")]
public sealed class LookupController : ControllerBase
{
    private readonly AreasApiClient _areas;
    private readonly IGestoresLookupService _gestores;

    public LookupController(AreasApiClient areas, IGestoresLookupService gestores)
    {
        _areas = areas;
        _gestores = gestores;
    }

    [HttpGet("areas")]
    public async Task<IActionResult> Areas(CancellationToken ct)
    {
        var tenantId = Request.Headers["X-Tenant-Id"].ToString();
        if (string.IsNullOrWhiteSpace(tenantId)) tenantId = "liotecnica";

        var areas = await _areas.GetAreasAsync(tenantId, ct);

        // já devolve enxuto e pronto pro select
        var result = areas
            .Where(a => a.IsActive)
            .Select(a => new { id = a.Id, code = a.Code, name = a.Name })
            .ToList();

        return Ok(result);
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
        // (opcional) normalização defensiva
        page = Math.Max(1, page);
        pageSize = Math.Clamp(pageSize, 5, 200);

        var resp = await _gestores.LookupAsync(q, onlyActive, page, pageSize, ct);
        return Ok(resp);
    }
}
