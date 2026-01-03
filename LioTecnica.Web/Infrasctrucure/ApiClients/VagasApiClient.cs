using System.Net.Http.Json;

namespace LioTecnica.Web.Infrastructure.ApiClients;

public sealed class VagasApiClient
{
    private readonly HttpClient _http;

    public VagasApiClient(HttpClient http) => _http = http;

    public async Task<IReadOnlyList<VagaDto>> GetVagasAsync(string tenantId, string? authorization, CancellationToken ct)
    {
        using var req = new HttpRequestMessage(HttpMethod.Get, "api/vagas");
        req.Headers.TryAddWithoutValidation("X-Tenant-Id", tenantId);

        if (!string.IsNullOrWhiteSpace(authorization))
            req.Headers.TryAddWithoutValidation("Authorization", authorization);

        using var res = await _http.SendAsync(req, ct);
        res.EnsureSuccessStatusCode();

        return (await res.Content.ReadFromJsonAsync<List<VagaDto>>(cancellationToken: ct)) ?? [];
    }

    public async Task<VagaDto?> GetVagaByIdAsync(string tenantId, string? authorization, Guid id, CancellationToken ct)
    {
        using var req = new HttpRequestMessage(HttpMethod.Get, $"api/vagas/{id}");
        req.Headers.TryAddWithoutValidation("X-Tenant-Id", tenantId);

        if (!string.IsNullOrWhiteSpace(authorization))
            req.Headers.TryAddWithoutValidation("Authorization", authorization);

        using var res = await _http.SendAsync(req, ct);
        if (res.StatusCode == System.Net.HttpStatusCode.NotFound) return null;

        res.EnsureSuccessStatusCode();
        return await res.Content.ReadFromJsonAsync<VagaDto>(cancellationToken: ct);
    }
}

// Ajuste esse DTO para bater com o que sua API devolve hoje
public sealed record VagaDto(
    Guid Id,
    string Codigo,
    string Titulo,
    string Status,

    Guid AreaId,
    string AreaCode,
    string AreaName,

    Guid DepartmentId,
    string DepartmentCode,
    string DepartmentName,

    string Modalidade,
    string Senioridade,
    int QuantidadeVagas,
    int MatchMinimoPercentual,

    bool Confidencial,
    bool Urgente,
    bool AceitaPcd,

    DateOnly DataInicio,
    DateOnly DataEncerramento,

    string Cidade,
    string Uf,

    DateTimeOffset CreatedAtUtc,
    DateTimeOffset UpdatedAtUtc
);

