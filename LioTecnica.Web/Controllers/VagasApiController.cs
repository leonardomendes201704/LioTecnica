using LioTecnica.Web.Infrastructure.ApiClients;
using Microsoft.AspNetCore.Mvc;

namespace LioTecnica.Web.Controllers;

[ApiController]
[Route("api/vagas")]
public sealed class VagasApiController : ControllerBase
{
    private readonly VagasApiClient _vagas;

    public VagasApiController(VagasApiClient vagas)
        => _vagas = vagas;

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var tenantId = Request.Headers["X-Tenant-Id"].ToString();
        if (string.IsNullOrWhiteSpace(tenantId)) tenantId = "liotecnica";

        // (opcional) repassa Authorization caso sua API exija JWT
        var auth = Request.Headers.Authorization.ToString();

        var list = await _vagas.GetVagasAsync(tenantId, auth, ct);
        return Ok(list);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
    {
        var tenantId = Request.Headers["X-Tenant-Id"].ToString();
        if (string.IsNullOrWhiteSpace(tenantId)) tenantId = "liotecnica";

        var auth = Request.Headers.Authorization.ToString();

        var vaga = await _vagas.GetVagaByIdAsync(tenantId, auth, id, ct);
        return vaga is null ? NotFound() : Ok(vaga);
    }
}
