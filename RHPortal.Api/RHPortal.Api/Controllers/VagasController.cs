using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RhPortal.Api.Infrastructure.Data;
using RHPortal.Api.Domain.Entities;
using RHPortal.Api.Domain.Enums;

namespace RhPortal.Api.Controllers;

[ApiController]
[Route("api/vagas")]
public sealed class VagasController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<VagaListItemResponse>>> List(
        [FromServices] AppDbContext db,
        [FromQuery] string? q,
        [FromQuery] VagaStatus? status,
        [FromQuery] Guid? areaId,
        [FromQuery] Guid? departmentId,
        CancellationToken ct)
    {
        IQueryable<Vaga> query = db.Vagas.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(q))
        {
            q = q.Trim();
            var like = $"%{q}%";

            query = query.Where(v =>
                (v.Codigo != null && EF.Functions.Like(v.Codigo, like)) ||
                EF.Functions.Like(v.Titulo, like) ||
                (v.Cidade != null && EF.Functions.Like(v.Cidade, like)) ||
                (v.Uf != null && EF.Functions.Like(v.Uf, like)) ||

                (v.Area != null &&
                    ((v.Area.Code != null && EF.Functions.Like(v.Area.Code, like)) ||
                     (v.Area.Name != null && EF.Functions.Like(v.Area.Name, like)))) ||

                (v.Department != null &&
                    ((v.Department.Code != null && EF.Functions.Like(v.Department.Code, like)) ||
                     (v.Department.Name != null && EF.Functions.Like(v.Department.Name, like))))
            );
        }

        if (status.HasValue)
            query = query.Where(v => v.Status == status.Value);

        if (areaId.HasValue && areaId.Value != Guid.Empty)
            query = query.Where(v => v.AreaId == areaId.Value);

        if (departmentId.HasValue && departmentId.Value != Guid.Empty)
            query = query.Where(v => v.DepartmentId == departmentId.Value);

        // ✅ sem ORDER BY no SQL (mantém seu workaround do SQLite + DateTimeOffset)
        var items = await query
            .Select(v => new VagaListItemResponse(
                v.Id,
                v.Codigo,
                v.Titulo,
                v.Status,

                v.AreaId,
                v.Area != null ? v.Area.Code : null,
                v.Area != null ? v.Area.Name : null,

                v.DepartmentId,
                v.Department != null ? v.Department.Code : null,
                v.Department != null ? v.Department.Name : null,

                v.Modalidade,
                v.Senioridade,
                v.QuantidadeVagas,
                v.MatchMinimoPercentual,
                v.Confidencial,
                v.Urgente,
                v.AceitaPcd,
                v.DataInicio,
                v.DataEncerramento,
                v.Cidade,
                v.Uf,

                // ✅ agregados (Caminho B)
                v.Requisitos.Count(),
                v.Requisitos.Count(r => r.Obrigatorio),

                v.CreatedAtUtc,
                v.UpdatedAtUtc
            ))
            .ToListAsync(ct);

        // ✅ Ordena em memória
        items = items
            .OrderByDescending(x => x.UpdatedAtUtc)
            .ThenByDescending(x => x.CreatedAtUtc)
            .ToList();

        return Ok(items);
    }
}
