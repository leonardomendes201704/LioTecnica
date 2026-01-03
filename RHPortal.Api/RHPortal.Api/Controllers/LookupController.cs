using LioTecnica.Api.Contracts.Lookups;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RhPortal.Api.Contracts.Common;
using RhPortal.Api.Domain.Enums;
using RhPortal.Api.Infrastructure.Data;

namespace RhPortal.Api.Controllers;

[ApiController]
[Route("api/lookup")]
public sealed class LookupController : ControllerBase
{
    private readonly AppDbContext _db;

    public LookupController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet("units")]
    public async Task<ActionResult<List<OptionResponse>>> Units(CancellationToken ct)
    {
        var items = await _db.Units
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .Select(x => new OptionResponse(x.Id, x.Code, x.Name))
            .ToListAsync(ct);

        return Ok(items);
    }

    [HttpGet("areas")]
    public async Task<ActionResult<List<OptionResponse>>> Areas(CancellationToken ct)
    {
        var items = await _db.Areas
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .Select(x => new OptionResponse(x.Id, x.Code, x.Name))
            .ToListAsync(ct);

        return Ok(items);
    }

    [HttpGet("job-positions")]
    public async Task<ActionResult<List<OptionResponse>>> JobPositions(
        [FromQuery] Guid? areaId,
        CancellationToken ct)
    {
        var q = _db.JobPositions.AsNoTracking();

        if (areaId.HasValue && areaId.Value != Guid.Empty)
            q = q.Where(x => x.AreaId == areaId.Value);

        var items = await q
            .OrderBy(x => x.Name)
            .Select(x => new OptionResponse(x.Id, x.Code, x.Name))
            .ToListAsync(ct);

        return Ok(items);
    }

    [HttpGet("managers")]
    public async Task<ActionResult<LookupResponse<ManagerLookupItem>>> Gestores(
        [FromQuery] string? q,
        [FromQuery] bool onlyActive = true,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20,
        CancellationToken ct = default)
    {
        page = Math.Max(1, page);
        pageSize = Math.Clamp(pageSize, 5, 200);

        var query = _db.Managers
            .AsNoTracking()
            .Include(x => x.JobPosition)
            .Include(x => x.Area)
            .Include(x => x.Unit)
            .AsQueryable();

        if (onlyActive)
        {
            query = query.Where(x => x.Status == ManagerStatus.Active);
        }

        if (!string.IsNullOrWhiteSpace(q))
        {
            q = q.Trim();
            var like = $"%{q}%";

            query = query.Where(x =>
                EF.Functions.Like(x.Name, like) ||
                (x.Email != null && EF.Functions.Like(x.Email, like)) ||
                (x.JobPosition != null && EF.Functions.Like(x.JobPosition.Name, like)) ||
                (x.Area != null && EF.Functions.Like(x.Area.Name, like)) ||
                (x.Unit != null && EF.Functions.Like(x.Unit.Name, like))
            );
        }

        var total = await query.CountAsync(ct);
        var skip = (page - 1) * pageSize;

        var items = await query
            .OrderBy(x => x.Name)
            .Skip(skip)
            .Take(pageSize)
            .Select(x => new ManagerLookupItem
            {
                Id = x.Id,
                Nome = x.Name,
                Email = x.Email,

                Cargo = x.JobPosition != null ? x.JobPosition.Name : null,
                Area = x.Area != null ? x.Area.Name : null,
                Unidade = x.Unit != null ? x.Unit.Name : null,

                Status = x.Status,

                Telefone = x.Phone,
                Headcount = x.Headcount,
                Observacao = x.Notes,

                CreatedAt = x.CreatedAtUtc,
                UpdatedAt = x.UpdatedAtUtc
            })
            .ToListAsync(ct);

        var count = items.Count;
        var hasMore = (skip + count) < total;

        return Ok(new LookupResponse<ManagerLookupItem>
        {
            Items = items,
            Total = total,
            HasMore = hasMore
        });
    }
}
