using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RhPortal.Api.Contracts.Reports;
using RhPortal.Api.Domain.Entities;
using RhPortal.Api.Domain.Enums;
using RhPortal.Api.Infrastructure.Data;

namespace RhPortal.Api.Controllers;

[ApiController]
[Route("api/reports")]
public sealed class ReportsController : ControllerBase
{
    [HttpGet("catalog")]
    public ActionResult<IReadOnlyList<ReportCatalogItemResponse>> GetCatalog()
    {
        var items = new List<ReportCatalogItemResponse>
        {
            new("r1", "bar-chart", "Entrada por Origem", "Quantidade de itens recebidos por origem no periodo.", "entrada"),
            new("r2", "exclamation-triangle", "Falhas de Processamento", "Principais causas de falha (quando houver).", "entrada"),
            new("r3", "people", "Pipeline RH (Status do Candidato)", "Distribuicao por status do candidato.", "candidatos"),
            new("r4", "briefcase", "Funil por Vaga", "Candidatos por vaga e status.", "vagas"),
            new("r5", "stars", "Ranking de Matching", "Top candidatos por percentual de match.", "matching")
        };

        return Ok(items);
    }

    [HttpGet("vagas")]
    public async Task<ActionResult<IReadOnlyList<ReportVagaLookupResponse>>> GetVagas(
        [FromServices] AppDbContext db,
        CancellationToken ct)
    {
        var items = await db.Vagas.AsNoTracking()
            .OrderBy(v => v.Titulo)
            .Select(v => new ReportVagaLookupResponse(v.Id, v.Codigo, v.Titulo))
            .ToListAsync(ct);

        return Ok(items);
    }

    [HttpGet("entrada-origem")]
    public async Task<ActionResult<ReportDataResponse>> GetEntradaPorOrigem(
        [FromServices] AppDbContext db,
        [FromQuery] string? period,
        [FromQuery] Guid? vagaId,
        [FromQuery] string? origem,
        [FromQuery] string? status,
        [FromQuery] string? q,
        CancellationToken ct)
    {
        var query = BuildInboxQuery(db, period, vagaId, origem, status, q);

        var list = await query
            .OrderByDescending(c => c.RecebidoEm)
            .Take(30)
            .Select(c => new
            {
                c.RecebidoEm,
                c.Origem,
                c.Remetente,
                c.Assunto,
                VagaTitulo = c.Vaga != null ? c.Vaga.Titulo : null,
                VagaCodigo = c.Vaga != null ? c.Vaga.Codigo : null,
                c.Status
            })
            .ToListAsync(ct);

        var countByFonte = list
            .GroupBy(c => c.Origem)
            .ToDictionary(g => g.Key, g => g.Count());

        var labels = new[] { "Email", "Pasta", "Upload" };
        var values = new List<int>
        {
            countByFonte.TryGetValue(InboxOrigem.Email, out var c0) ? c0 : 0,
            countByFonte.TryGetValue(InboxOrigem.Pasta, out var c1) ? c1 : 0,
            countByFonte.TryGetValue(InboxOrigem.Upload, out var c2) ? c2 : 0
        };

        var headers = new[] { "Recebido em", "Origem", "Remetente", "Assunto", "Vaga", "Status" };
        var rows = list.Select(c => new List<ReportCellResponse>
        {
            MakeCell(c.RecebidoEm.ToString("dd/MM/yyyy")),
            MakeCell(MapInboxOrigem(c.Origem)),
            MakeCell(c.Remetente),
            MakeCell(c.Assunto, "mono"),
            MakeCell(FormatVaga(c.VagaTitulo, c.VagaCodigo)),
            MakeInboxStatusCell(c.Status)
        }).ToList();

        return Ok(new ReportDataResponse(labels, values, headers, rows));
    }

    [HttpGet("falhas-processamento")]
    public async Task<ActionResult<ReportDataResponse>> GetFalhasProcessamento(
        [FromServices] AppDbContext db,
        [FromQuery] string? period,
        [FromQuery] Guid? vagaId,
        [FromQuery] string? origem,
        [FromQuery] string? status,
        [FromQuery] string? q,
        CancellationToken ct)
    {
        var query = BuildInboxQuery(db, period, vagaId, origem, status, q)
            .Where(x => x.Status == InboxStatus.Falha);

        var list = await query
            .OrderByDescending(x => x.RecebidoEm)
            .Take(30)
            .Select(x => new
            {
                x.RecebidoEm,
                x.Origem,
                x.Assunto,
                VagaTitulo = x.Vaga != null ? x.Vaga.Titulo : null,
                VagaCodigo = x.Vaga != null ? x.Vaga.Codigo : null,
                x.ProcessamentoUltimoErro
            })
            .ToListAsync(ct);

        var grouped = list
            .GroupBy(x => string.IsNullOrWhiteSpace(x.ProcessamentoUltimoErro) ? "Outros" : x.ProcessamentoUltimoErro)
            .OrderByDescending(x => x.Count())
            .Take(6)
            .ToList();

        var labels = grouped.Select(g => g.Key.Length > 22 ? g.Key[..22] + "..." : g.Key).ToList();
        var values = grouped.Select(g => g.Count()).ToList();

        var headers = new[] { "Recebido em", "Origem", "Assunto", "Vaga", "Erro" };
        var rows = list.Select(x => new List<ReportCellResponse>
        {
            MakeCell(x.RecebidoEm.ToString("dd/MM/yyyy")),
            MakeCell(MapInboxOrigem(x.Origem)),
            MakeCell(x.Assunto),
            MakeCell(FormatVaga(x.VagaTitulo, x.VagaCodigo)),
            MakeCell(x.ProcessamentoUltimoErro ?? "-")
        }).ToList();

        return Ok(new ReportDataResponse(labels, values, headers, rows));
    }

    [HttpGet("pipeline-status")]
    public async Task<ActionResult<ReportDataResponse>> GetPipelineStatus(
        [FromServices] AppDbContext db,
        [FromQuery] string? period,
        [FromQuery] Guid? vagaId,
        [FromQuery] string? origem,
        [FromQuery] string? status,
        [FromQuery] string? q,
        CancellationToken ct)
    {
        var query = BuildCandidateQuery(db, period, vagaId, origem, status, q);

        var list = await query
            .OrderByDescending(c => c.CreatedAtUtc)
            .Take(30)
            .Select(c => new
            {
                c.CreatedAtUtc,
                c.Nome,
                c.Email,
                c.Status,
                VagaTitulo = c.Vaga != null ? c.Vaga.Titulo : null,
                VagaCodigo = c.Vaga != null ? c.Vaga.Codigo : null
            })
            .ToListAsync(ct);

        var grouped = await query
            .GroupBy(c => c.Status)
            .Select(g => new { Status = g.Key, Count = g.Count() })
            .ToListAsync(ct);

        var labels = grouped.Select(g => MapStatusLabel(g.Status)).ToList();
        var values = grouped.Select(g => g.Count).ToList();

        var headers = new[] { "Criado em", "Nome", "Email", "Status", "Vaga" };
        var rows = list.Select(c => new List<ReportCellResponse>
        {
            MakeCell(c.CreatedAtUtc.ToString("dd/MM/yyyy")),
            MakeCell(c.Nome),
            MakeCell(c.Email, "mono"),
            MakeStatusCell(c.Status),
            MakeCell(FormatVaga(c.VagaTitulo, c.VagaCodigo))
        }).ToList();

        return Ok(new ReportDataResponse(labels, values, headers, rows));
    }

    [HttpGet("funil-vaga")]
    public async Task<ActionResult<ReportDataResponse>> GetFunilPorVaga(
        [FromServices] AppDbContext db,
        [FromQuery] string? period,
        [FromQuery] Guid? vagaId,
        [FromQuery] string? origem,
        [FromQuery] string? status,
        [FromQuery] string? q,
        CancellationToken ct)
    {
        var query = BuildCandidateQuery(db, period, vagaId, origem, status, q);

        var grouped = await query
            .GroupBy(c => new { c.VagaId, Titulo = c.Vaga != null ? c.Vaga.Titulo : null, Codigo = c.Vaga != null ? c.Vaga.Codigo : null })
            .Select(g => new
            {
                g.Key.VagaId,
                g.Key.Titulo,
                g.Key.Codigo,
                Total = g.Count(),
                Triagem = g.Count(x => x.Status == CandidatoStatus.Triagem),
                Aprovados = g.Count(x => x.Status == CandidatoStatus.Aprovado),
                Reprovados = g.Count(x => x.Status == CandidatoStatus.Reprovado)
            })
            .OrderByDescending(x => x.Total)
            .Take(8)
            .ToListAsync(ct);

        var labels = grouped.Select(g => g.Codigo ?? "Vaga").ToList();
        var values = grouped.Select(g => g.Total).ToList();

        var headers = new[] { "Vaga", "Recebidos", "Triagem", "Aprovados", "Reprovados", "Taxa OK" };
        var rows = grouped.Select(g =>
        {
            var rate = g.Total > 0 ? (int)Math.Round((double)g.Aprovados / g.Total * 100) : 0;
            var badge = rate >= 70
                ? MakeTagCell($"{rate}%", "ok", "bi-check2-circle")
                : rate >= 40
                    ? MakeTagCell($"{rate}%", "warn", "bi-exclamation-circle")
                    : MakeTagCell($"{rate}%", "bad", "bi-x-circle");

            return new List<ReportCellResponse>
            {
                MakeCell(FormatVaga(g.Titulo, g.Codigo)),
                MakeCell(g.Total.ToString(), "fw-semibold"),
                MakeCell(g.Triagem.ToString(), "fw-semibold"),
                MakeCell(g.Aprovados.ToString(), "fw-semibold text-success"),
                MakeCell(g.Reprovados.ToString(), "fw-semibold text-danger"),
                badge
            };
        }).ToList();

        return Ok(new ReportDataResponse(labels, values, headers, rows));
    }

    [HttpGet("ranking-matching")]
    public async Task<ActionResult<ReportDataResponse>> GetRankingMatching(
        [FromServices] AppDbContext db,
        [FromQuery] string? period,
        [FromQuery] Guid? vagaId,
        [FromQuery] string? origem,
        [FromQuery] string? status,
        [FromQuery] string? q,
        [FromQuery] int take,
        CancellationToken ct)
    {
        var safeTake = Math.Clamp(take <= 0 ? 12 : take, 1, 50);
        var query = BuildCandidateQuery(db, period, vagaId, origem, status, q)
            .Where(c => c.LastMatchScore != null);

        var list = await query
            .OrderByDescending(c => c.LastMatchScore)
            .ThenByDescending(c => c.UpdatedAtUtc)
            .Take(safeTake)
            .Select(c => new
            {
                c.Nome,
                c.Email,
                VagaTitulo = c.Vaga != null ? c.Vaga.Titulo : null,
                VagaCodigo = c.Vaga != null ? c.Vaga.Codigo : null,
                MatchScore = c.LastMatchScore ?? 0,
                UpdatedAt = c.UpdatedAtUtc
            })
            .ToListAsync(ct);

        var labels = list.Take(6).Select(c => (c.Nome ?? "").Split(' ', StringSplitOptions.RemoveEmptyEntries).FirstOrDefault() ?? "-").ToList();
        var values = list.Take(6).Select(c => c.MatchScore).ToList();

        var headers = new[] { "Candidato", "Email", "Vaga", "Match", "Atualizado" };
        var rows = list.Select(c =>
        {
            var badge = c.MatchScore >= 80
                ? MakeTagCell($"{c.MatchScore}%", "ok", "bi-stars")
                : c.MatchScore >= 60
                    ? MakeTagCell($"{c.MatchScore}%", "warn", "bi-stars")
                    : MakeTagCell($"{c.MatchScore}%", "bad", "bi-stars");

            return new List<ReportCellResponse>
            {
                MakeCell(c.Nome),
                MakeCell(c.Email, "mono"),
                MakeCell(FormatVaga(c.VagaTitulo, c.VagaCodigo)),
                badge,
                MakeCell(c.UpdatedAt.ToString("dd/MM/yyyy"))
            };
        }).ToList();

        return Ok(new ReportDataResponse(labels, values, headers, rows));
    }

    private static IQueryable<RhPortal.Api.Domain.Entities.Candidato> BuildCandidateQuery(
        AppDbContext db,
        string? period,
        Guid? vagaId,
        string? origem,
        string? status,
        string? q)
    {
        var start = PeriodStart(period);
        var query = db.Candidatos.AsNoTracking()
            .Include(c => c.Vaga)
            .Where(c => c.CreatedAtUtc >= start);

        if (vagaId.HasValue)
            query = query.Where(c => c.VagaId == vagaId.Value);

        if (TryParseEnum<CandidatoFonte>(origem, out var fonte))
            query = query.Where(c => c.Fonte == fonte);

        if (TryParseEnum<CandidatoStatus>(status, out var candStatus))
            query = query.Where(c => c.Status == candStatus);

        if (!string.IsNullOrWhiteSpace(q))
        {
            var text = q.Trim().ToLower();
            query = query.Where(c =>
                c.Nome.ToLower().Contains(text) ||
                c.Email.ToLower().Contains(text) ||
                (c.Vaga != null && ((c.Vaga.Titulo ?? "").ToLower().Contains(text) ||
                                    (c.Vaga.Codigo ?? "").ToLower().Contains(text))));
        }

        return query;
    }

    private static IQueryable<InboxItem> BuildInboxQuery(
        AppDbContext db,
        string? period,
        Guid? vagaId,
        string? origem,
        string? status,
        string? q)
    {
        var start = PeriodStart(period);
        var query = db.InboxItems.AsNoTracking()
            .Include(x => x.Vaga)
            .Where(x => x.RecebidoEm >= start);

        if (vagaId.HasValue)
            query = query.Where(x => x.VagaId == vagaId.Value);

        if (TryParseEnum<InboxOrigem>(origem, out var parsedOrigem))
            query = query.Where(x => x.Origem == parsedOrigem);

        if (TryParseEnum<InboxStatus>(status, out var parsedStatus))
            query = query.Where(x => x.Status == parsedStatus);

        if (!string.IsNullOrWhiteSpace(q))
        {
            var text = q.Trim().ToLower();
            query = query.Where(x =>
                (x.Remetente ?? "").ToLower().Contains(text) ||
                (x.Assunto ?? "").ToLower().Contains(text) ||
                (x.Destinatario ?? "").ToLower().Contains(text));
        }

        return query;
    }

    private static DateTimeOffset PeriodStart(string? period)
    {
        var now = DateTimeOffset.UtcNow;
        return period switch
        {
            "7d" => now.AddDays(-7),
            "30d" => now.AddDays(-30),
            "90d" => now.AddDays(-90),
            "ytd" => new DateTimeOffset(new DateTime(now.Year, 1, 1), TimeSpan.Zero),
            _ => now.AddDays(-30)
        };
    }

    private static bool TryParseEnum<TEnum>(string? value, out TEnum result) where TEnum : struct
    {
        result = default;
        if (string.IsNullOrWhiteSpace(value) || value == "all")
            return false;
        return Enum.TryParse(value, true, out result);
    }

    private static string MapOrigem(CandidatoFonte fonte)
    {
        return fonte switch
        {
            CandidatoFonte.Email => "Email",
            CandidatoFonte.Pasta => "Pasta",
            CandidatoFonte.LinkedIn => "LinkedIn",
            CandidatoFonte.Indicacao => "Indicacao",
            CandidatoFonte.Site => "Site",
            _ => "Outro"
        };
    }

    private static string MapInboxOrigem(InboxOrigem origem)
    {
        return origem switch
        {
            InboxOrigem.Email => "Email",
            InboxOrigem.Pasta => "Pasta",
            InboxOrigem.Upload => "Upload",
            _ => "Outro"
        };
    }

    private static string MapStatusLabel(CandidatoStatus status)
    {
        return status switch
        {
            CandidatoStatus.Novo => "Novo",
            CandidatoStatus.Triagem => "Triagem",
            CandidatoStatus.Pendente => "Pendente",
            CandidatoStatus.Aprovado => "Aprovado",
            CandidatoStatus.Reprovado => "Reprovado",
            _ => "Outro"
        };
    }

    private static ReportCellResponse MakeCell(string? text, string? className = null)
        => new(text ?? string.Empty, className, null);

    private static ReportCellResponse MakeTagCell(string text, string cls, string icon)
        => new(text, new[] { "tag", cls }.Where(x => !string.IsNullOrWhiteSpace(x)).Aggregate(string.Empty, (a,b) => string.IsNullOrEmpty(a) ? b : $"{a} {b}"), icon);

    private static ReportCellResponse MakeStatusCell(CandidatoStatus status)
    {
        var label = MapStatusLabel(status);
        return status switch
        {
            CandidatoStatus.Aprovado => MakeTagCell(label, "ok", "bi-check2-circle"),
            CandidatoStatus.Reprovado => MakeTagCell(label, "bad", "bi-x-circle"),
            CandidatoStatus.Pendente => MakeTagCell(label, "warn", "bi-exclamation-circle"),
            _ => MakeTagCell(label, string.Empty, "bi-dot")
        };
    }

    private static ReportCellResponse MakeInboxStatusCell(InboxStatus status)
    {
        var label = status switch
        {
            InboxStatus.Novo => "Novo",
            InboxStatus.Processando => "Processando",
            InboxStatus.Processado => "Processado",
            InboxStatus.Falha => "Falha",
            InboxStatus.Descartado => "Descartado",
            _ => "Outro"
        };

        return status switch
        {
            InboxStatus.Processado => MakeTagCell(label, "ok", "bi-check2-circle"),
            InboxStatus.Processando => MakeTagCell(label, "warn", "bi-arrow-repeat"),
            InboxStatus.Falha => MakeTagCell(label, "bad", "bi-exclamation-triangle"),
            InboxStatus.Descartado => MakeTagCell(label, "bad", "bi-trash3"),
            _ => MakeTagCell(label, string.Empty, "bi-dot")
        };
    }

    private static string FormatVaga(string? titulo, string? codigo)
    {
        var title = string.IsNullOrWhiteSpace(titulo) ? "-" : titulo;
        var code = string.IsNullOrWhiteSpace(codigo) ? "-" : codigo;
        return $"{title} ({code})";
    }
}
