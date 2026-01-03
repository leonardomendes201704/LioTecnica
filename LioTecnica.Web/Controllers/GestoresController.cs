using LioTecnica.Web.Helpers;
using LioTecnica.Web.Services;
using LioTecnica.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LioTecnica.Web.Controllers;

public class GestoresController : Controller
{
    private readonly IGestoresLookupService _lookup;

    public GestoresController(IGestoresLookupService lookup)
    {
        _lookup = lookup;
    }

    public async Task<IActionResult> Index(string? q = null)
    {
        var seed = MockDataService.BuildSeedBundle();

        // busca real
        var lookup = await _lookup.LookupAsync(q, onlyActive: false, page: 1, pageSize: 200);

        var gestores = lookup.Items.Select(x => new LioTecnica.Web.ViewModels.Seed.GestorSeed
        {
            Id = x.Id.ToString(),
            Nome = x.Nome,
            Email = x.Email ?? "",
            Cargo = x.Cargo ?? "",
            Area = x.Area ?? "",
            Unidade = x.Unidade ?? "",
            Status = x.Status ?? "ativo",

            Telefone = x.Telefone ?? "",
            Headcount = x.Headcount,
            Observacao = x.Observacao ?? "",

            CreatedAt = (x.CreatedAt ?? DateTimeOffset.UtcNow).ToString("o"),
            UpdatedAt = (x.UpdatedAt ?? DateTimeOffset.UtcNow).ToString("o"),
        }).ToList();

        // ✅ record: cria uma cópia alterando só Gestores
        var seed2 = seed with { Gestores = gestores };

        var model = new PageSeedViewModel
        {
            SeedJson = SeedJsonHelper.ToJson(seed2)
        };

        return View(model);
    }
}
