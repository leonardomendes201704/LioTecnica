using Microsoft.EntityFrameworkCore;
using RhPortal.Api.Contracts.Candidatos;
using RhPortal.Api.Domain.Entities;
using RhPortal.Api.Infrastructure.Data;

namespace RhPortal.Api.Application.Candidatos;

public interface ICandidatoService
{
    Task<IReadOnlyList<CandidatoListItemResponse>> ListAsync(CandidatoListQuery query, CancellationToken ct);
    Task<CandidatoResponse?> GetByIdAsync(Guid id, CancellationToken ct);
    Task<CandidatoResponse> CreateAsync(CandidatoCreateRequest request, CancellationToken ct);
    Task<CandidatoResponse?> UpdateAsync(Guid id, CandidatoUpdateRequest request, CancellationToken ct);
    Task<bool> DeleteAsync(Guid id, CancellationToken ct);
}

public sealed class CandidatoService : ICandidatoService
{
    private readonly AppDbContext _db;

    public CandidatoService(AppDbContext db) => _db = db;

    public async Task<IReadOnlyList<CandidatoListItemResponse>> ListAsync(CandidatoListQuery query, CancellationToken ct)
    {
        IQueryable<Candidato> q = _db.Candidatos
            .AsNoTracking()
            .Include(x => x.Vaga);

        if (!string.IsNullOrWhiteSpace(query.Q))
        {
            var term = query.Q.Trim();
            var like = $"%{term}%";

            q = q.Where(c =>
                EF.Functions.Like(c.Nome, like) ||
                EF.Functions.Like(c.Email, like) ||
                (c.Fone != null && EF.Functions.Like(c.Fone, like)) ||
                (c.Cidade != null && EF.Functions.Like(c.Cidade, like)) ||
                (c.Uf != null && EF.Functions.Like(c.Uf, like)) ||
                (c.Obs != null && EF.Functions.Like(c.Obs, like)) ||
                (c.Vaga != null &&
                    ((c.Vaga.Codigo != null && EF.Functions.Like(c.Vaga.Codigo, like)) ||
                     EF.Functions.Like(c.Vaga.Titulo, like)))
            );
        }

        if (query.Status.HasValue)
            q = q.Where(c => c.Status == query.Status.Value);

        if (query.Fonte.HasValue)
            q = q.Where(c => c.Fonte == query.Fonte.Value);

        if (query.VagaId.HasValue && query.VagaId.Value != Guid.Empty)
            q = q.Where(c => c.VagaId == query.VagaId.Value);

        var items = await q
            .Select(c => new CandidatoListItemResponse(
                c.Id,
                c.Nome,
                c.Email,
                c.Fone,
                c.Cidade,
                c.Uf,
                c.Fonte,
                c.Status,
                c.VagaId,
                c.Vaga != null ? c.Vaga.Codigo : null,
                c.Vaga != null ? c.Vaga.Titulo : null,
                c.Obs,
                c.CvText,
                MapMatch(c),
                c.CreatedAtUtc,
                c.UpdatedAtUtc
            ))
            .ToListAsync(ct);

        return items
            .OrderByDescending(x => x.UpdatedAtUtc)
            .ThenByDescending(x => x.CreatedAtUtc)
            .ToList();
    }

    public async Task<CandidatoResponse?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        var entity = await _db.Candidatos
            .AsNoTracking()
            .Include(x => x.Vaga)
            .Include(x => x.Documentos)
            .FirstOrDefaultAsync(x => x.Id == id, ct);

        return entity is null ? null : MapToResponse(entity);
    }

    public async Task<CandidatoResponse> CreateAsync(CandidatoCreateRequest request, CancellationToken ct)
    {
        await EnsureVagaAsync(request.VagaId, ct);

        var entity = new Candidato
        {
            Id = Guid.NewGuid(),
            Nome = (request.Nome ?? string.Empty).Trim(),
            Email = NormalizeEmail(request.Email),
            Fone = TrimOrNull(request.Fone),
            Cidade = TrimOrNull(request.Cidade),
            Uf = NormalizeUf(request.Uf),
            Fonte = request.Fonte,
            Status = request.Status,
            VagaId = request.VagaId,
            Obs = TrimOrNull(request.Obs),
            CvText = TrimOrNull(request.CvText)
        };

        entity.Documentos = BuildDocumentos(request.Documentos, entity.Id);

        ApplyLastMatch(entity, request.LastMatch);

        _db.Candidatos.Add(entity);
        await _db.SaveChangesAsync(ct);

        return (await GetByIdAsync(entity.Id, ct))!;
    }

    public async Task<CandidatoResponse?> UpdateAsync(Guid id, CandidatoUpdateRequest request, CancellationToken ct)
    {
        var entity = await _db.Candidatos
            .Include(x => x.Documentos)
            .FirstOrDefaultAsync(x => x.Id == id, ct);

        if (entity is null) return null;

        await EnsureVagaAsync(request.VagaId, ct);

        entity.Nome = (request.Nome ?? string.Empty).Trim();
        entity.Email = NormalizeEmail(request.Email);
        entity.Fone = TrimOrNull(request.Fone);
        entity.Cidade = TrimOrNull(request.Cidade);
        entity.Uf = NormalizeUf(request.Uf);
        entity.Fonte = request.Fonte;
        entity.Status = request.Status;
        entity.VagaId = request.VagaId;
        entity.Obs = TrimOrNull(request.Obs);
        entity.CvText = TrimOrNull(request.CvText);

        ApplyLastMatch(entity, request.LastMatch);

        if (request.Documentos is not null)
        {
            if (entity.Documentos.Count > 0)
                _db.CandidatoDocumentos.RemoveRange(entity.Documentos);

            entity.Documentos = BuildDocumentos(request.Documentos, entity.Id);
        }

        await _db.SaveChangesAsync(ct);
        return await GetByIdAsync(id, ct);
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken ct)
    {
        var entity = await _db.Candidatos.FirstOrDefaultAsync(x => x.Id == id, ct);
        if (entity is null) return false;

        _db.Candidatos.Remove(entity);
        await _db.SaveChangesAsync(ct);
        return true;
    }

    private async Task EnsureVagaAsync(Guid vagaId, CancellationToken ct)
    {
        var exists = await _db.Vagas.AnyAsync(v => v.Id == vagaId, ct);
        if (!exists) throw new InvalidOperationException("Vaga invalida.");
    }

    private static CandidatoResponse MapToResponse(Candidato c)
    {
        return new CandidatoResponse(
            c.Id,
            c.Nome,
            c.Email,
            c.Fone,
            c.Cidade,
            c.Uf,
            c.Fonte,
            c.Status,
            c.VagaId,
            c.Vaga != null ? c.Vaga.Codigo : null,
            c.Vaga != null ? c.Vaga.Titulo : null,
            c.Obs,
            c.CvText,
            MapMatch(c),
            c.Documentos.OrderByDescending(x => x.CreatedAtUtc).Select(MapDocumento).ToList(),
            c.CreatedAtUtc,
            c.UpdatedAtUtc
        );
    }

    private static CandidatoDocumentoResponse MapDocumento(CandidatoDocumento d)
        => new(
            d.Id,
            d.Tipo,
            d.NomeArquivo,
            d.ContentType,
            d.Descricao,
            d.TamanhoBytes,
            d.Url,
            d.CreatedAtUtc,
            d.UpdatedAtUtc
        );

    private static CandidatoMatchResponse? MapMatch(Candidato c)
    {
        if (c.LastMatchScore is null && c.LastMatchPass is null && c.LastMatchAtUtc is null && c.LastMatchVagaId is null)
            return null;

        return new CandidatoMatchResponse(
            c.LastMatchScore,
            c.LastMatchPass,
            c.LastMatchAtUtc,
            c.LastMatchVagaId
        );
    }

    private static void ApplyLastMatch(Candidato entity, CandidatoMatchRequest? match)
    {
        if (match is null)
        {
            entity.LastMatchScore = null;
            entity.LastMatchPass = null;
            entity.LastMatchAtUtc = null;
            entity.LastMatchVagaId = null;
            return;
        }

        entity.LastMatchScore = match.Score;
        entity.LastMatchPass = match.Pass;
        entity.LastMatchAtUtc = match.AtUtc;
        entity.LastMatchVagaId = match.VagaId;
    }

    private static List<CandidatoDocumento> BuildDocumentos(IReadOnlyList<CandidatoDocumentoRequest>? items, Guid candidatoId)
    {
        if (items is null || items.Count == 0) return [];

        var list = new List<CandidatoDocumento>(items.Count);
        for (var i = 0; i < items.Count; i++)
        {
            var item = items[i];
            list.Add(new CandidatoDocumento
            {
                Id = Guid.NewGuid(),
                CandidatoId = candidatoId,
                Tipo = item.Tipo,
                NomeArquivo = (item.NomeArquivo ?? string.Empty).Trim(),
                ContentType = TrimOrNull(item.ContentType),
                Descricao = TrimOrNull(item.Descricao),
                TamanhoBytes = item.TamanhoBytes,
                Url = TrimOrNull(item.Url)
            });
        }
        return list;
    }

    private static string NormalizeEmail(string? email)
        => (email ?? string.Empty).Trim().ToLowerInvariant();

    private static string? NormalizeUf(string? uf)
    {
        var text = (uf ?? string.Empty).Trim();
        return string.IsNullOrWhiteSpace(text) ? null : text.ToUpperInvariant();
    }

    private static string? TrimOrNull(string? value)
        => string.IsNullOrWhiteSpace(value) ? null : value.Trim();
}
