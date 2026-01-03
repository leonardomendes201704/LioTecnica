using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RhPortal.Api.Contracts.Areas;
using RhPortal.Api.Infrastructure.Data;

namespace RhPortal.Api.Controllers;

[ApiController]
[Route("api/areas")]
public sealed class AreasController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<AreaResponse>>> List([FromServices] AppDbContext db, CancellationToken ct)
    {
        var items = await db.Areas
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .Select(x => new AreaResponse(x.Id, x.Code, x.Name, x.IsActive))
            .ToListAsync(ct);

        return Ok(items);
    }
}
