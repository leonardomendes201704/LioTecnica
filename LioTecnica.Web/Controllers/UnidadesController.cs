using LioTecnica.Web.Infrastructure.Security;
using LioTecnica.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using RhPortal.Web.Infrastructure.ApiClients;

namespace RhPortal.Web.Controllers;

public sealed class UnidadesController : Controller
{
    private readonly UnitsApiClient _unitsApi;
    private readonly ManagersApiClient _managersApi;
    private readonly PortalTenantContext _tenantContext;

    public UnidadesController(UnitsApiClient unitsApi, ManagersApiClient managersApi, PortalTenantContext tenantContext)
    {
        _unitsApi = unitsApi;
        _managersApi = managersApi;
        _tenantContext = tenantContext;
    }

    [HttpGet("/Unidades")]
    public IActionResult Index()
    {
        var vm = new PageSeedViewModel
        {
            SeedJson = "{}"
        };

        return View("Index", vm);
    }

    [HttpGet("/Unidades/_api")]
    public async Task<IActionResult> List(CancellationToken ct)
    {
        var tenantId = _tenantContext.TenantId;
        var api = await _unitsApi.GetUnitsAsync(tenantId, ct);

        var items = (api?.Items is { Count: > 0 })
            ? api.Items.Select(MapToFrontUnidade).ToList()
            : new List<object>();

        return Ok(new
        {
            items,
            api.Page,
            api.PageSize,
            api.TotalItems,
            api.TotalPages
        });
    }

    [HttpGet("/Unidades/_api/{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken ct)
    {
        var tenantId = _tenantContext.TenantId;
        var item = await _unitsApi.GetByIdAsync(tenantId, id, ct);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost("/Unidades/_api")]
    public async Task<IActionResult> Create([FromBody] UnitCreateRequest request, CancellationToken ct)
    {
        var tenantId = _tenantContext.TenantId;
        var created = await _unitsApi.CreateAsync(tenantId, request, ct);
        return Ok(created);
    }

    [HttpPut("/Unidades/_api/{id:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UnitUpdateRequest request, CancellationToken ct)
    {
        var tenantId = _tenantContext.TenantId;
        var updated = await _unitsApi.UpdateAsync(tenantId, id, request, ct);
        return updated is null ? NotFound() : Ok(updated);
    }

    [HttpDelete("/Unidades/_api/{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken ct)
    {
        var tenantId = _tenantContext.TenantId;
        var ok = await _unitsApi.DeleteAsync(tenantId, id, ct);
        return ok ? NoContent() : NotFound();
    }

    [HttpGet("/api/lookup/units")]
    public async Task<IActionResult> UnitsLookup(CancellationToken ct)
    {
        var tenantId = _tenantContext.TenantId;

        var api = await _unitsApi.GetUnitsAsync(tenantId, ct);

        var items = (api?.Items is { Count: > 0 })
            ? api.Items
                .Select(u => (object)new
                {
                    id = u.Id.ToString(),
                    code = u.Code ?? "",
                    name = u.Name ?? ""
                })
                .ToList()
            : new List<object>();

        return Ok(items); // array direto
    }

    // /Unidades/{id}/gestores -> web chama API e devolve JSON pro JS
    [HttpGet("/Unidades/{id:guid}/gestores")]
    public async Task<IActionResult> GestoresDaUnidade([FromRoute] Guid id, CancellationToken ct)
    {
        var tenantId = _tenantContext.TenantId;

        // chama API de gestores filtrando por UnitId
        var api = await _managersApi.GetManagersAsync(
            tenantId,
            unitId: id,
            page: 1,
            pageSize: 200,
            ct: ct
        );

        // devolve no formato que teu JS j? espera
        var items = api.Items.Select(m => new {
            id = m.Id,
            nome = m.Name,
            email = m.Email,
            cargo = m.JobTitle,
            area = m.Area,
            unidade = m.UnitName,
            status = (m.Status?.Equals("Active", StringComparison.OrdinalIgnoreCase) ?? false) ? "ativo" : "inativo",
            headcount = m.Headcount
        });

        return Ok(new { items });
    }

    // ====== MAPEAMENTO ======
    private static object MapToFrontUnidade(UnitApiItem u)
    {
        static string MapStatus(string? s)
        {
            if (string.IsNullOrWhiteSpace(s)) return "inativo";
            return s.Equals("Active", StringComparison.OrdinalIgnoreCase) ? "ativo" : "inativo";
        }

        // O JS usa:
        // id, codigo, nome, status, cidade, uf, endereco, bairro, cep, email, telefone, responsavel, tipo, headcount, observacao
        return new
        {
            id = u.Id.ToString(),
            codigo = u.Code ?? "",
            nome = u.Name ?? "",
            status = MapStatus(u.Status),
            headcount = u.Headcount,
            email = u.Email ?? "",
            telefone = u.Phone ?? "",
            tipo = u.Type ?? "",
            cidade = u.City ?? "",
            uf = (u.Uf ?? "").ToUpperInvariant(),

            endereco = u.AddressLine ?? "",
            bairro = u.Neighborhood ?? "",
            cep = u.ZipCode ?? "",
            responsavel = u.ResponsibleName ?? "",
            observacao = u.Notes ?? "",
        };
    }
}
