using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using LioTecnica.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using RhPortal.Web.Infrastructure.ApiClients;

namespace RhPortal.Web.Controllers;

public sealed class UnidadesController : Controller
{
    private readonly UnitsApiClient _unitsApi;
    private readonly ManagersApiClient _managersApi;

    public UnidadesController(UnitsApiClient unitsApi, ManagersApiClient managersApi)
    {
        _unitsApi = unitsApi;
        _managersApi = managersApi; 
    }

    [HttpGet("/Unidades")]
    public async Task<IActionResult> Index(CancellationToken ct)
    {
        // 1) Tenant
        var tenantId =
            (Request.Headers.TryGetValue("X-Tenant-Id", out var h) ? h.ToString() : null)
            ?? "liotecnica";

        // 2) Seed base (provavelmente anonymous type: new { unidades = ..., vagas = ..., gestores = ... })
        var baseSeed = BuildBaseSeed();

        // 3) API
        var api = await _unitsApi.GetUnitsAsync(tenantId, ct);

        // 4) Unidades no formato do front
        var unidadesForFront = (api?.Items is { Count: > 0 })
            ? api.Items.Select(MapToFrontUnidade).ToList()
            : new List<object>();

        // 5) Pegar vagas/gestores do seed base (sem dynamic)
        static object? GetProp(object obj, string name)
            => obj.GetType()
                  .GetProperty(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase)
                  ?.GetValue(obj);

        var vagasBase = GetProp(baseSeed, "vagas") ?? Array.Empty<object>();
        var gestoresBase = GetProp(baseSeed, "gestores") ?? Array.Empty<object>();

        // 6) Objeto FINAL (imutável) já com as unidades corretas
        var seedFinal = new
        {
            unidades = unidadesForFront,
            vagas = vagasBase,
            gestores = gestoresBase
        };

        // 7) Serializar
        var vm = new PageSeedViewModel
        {
            SeedJson = JsonSerializer.Serialize(seedFinal, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            })
        };

        return View("Index", vm);
    }

    [HttpGet("/api/lookup/units")]
    public async Task<IActionResult> UnitsLookup(CancellationToken ct)
    {
        var tenantId =
            (Request.Headers.TryGetValue("X-Tenant-Id", out var h) ? h.ToString() : null)
            ?? "liotecnica";

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
        var tenantId =
            (Request.Headers.TryGetValue("X-Tenant-Id", out var h) ? h.ToString() : null)
            ?? "liotecnica";

        // chama API de gestores filtrando por UnitId
        var api = await _managersApi.GetManagersAsync(
            tenantId,
            unitId: id,
            page: 1,
            pageSize: 200,
            ct: ct
        );

        // devolve no formato que teu JS já espera
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

            // Esses hoje não vêm no /api/units (ainda). Vai ficar "-" no modal até você expor no backend.
            endereco = u.AddressLine ?? "",
            bairro = u.Neighborhood ?? "",
            cep = u.ZipCode ?? "",
            responsavel = u.ResponsibleName ?? "",
            observacao = u.Notes ?? "",
        };
    }

    // ====== SEED BASE (EXEMPLO) ======
    // Aqui você deve plugar no seu SeedBuilder real.
    private static dynamic BuildBaseSeed()
    {
        // IMPORTANTE: manter vagas/gestores no seed pq seu modal de unidade usa loadVagas/loadGestores do localStorage/seed.
        return new
        {
            gestores = Array.Empty<object>(),
            vagas = Array.Empty<object>(),
            unidades = Array.Empty<object>()
        };
    }
}
