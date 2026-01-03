using System.Net.Http.Headers;

namespace LioTecnica.Web.Services;

public interface IGestoresLookupService
{
    Task<LookupResponse<GestorLookupItem>> LookupAsync(
        string? q,
        bool onlyActive = true,
        int page = 1,
        int pageSize = 50,
        CancellationToken ct = default);
}

public sealed class GestoresLookupService : IGestoresLookupService
{
    private const string TenantHeaderName = "X-Tenant-Id";
    private const string TenantId = "liotecnica";

    private readonly HttpClient _http;

    public GestoresLookupService(HttpClient http) => _http = http;

    public async Task<LookupResponse<GestorLookupItem>> LookupAsync(
        string? q,
        bool onlyActive = true,
        int page = 1,
        int pageSize = 50,
        CancellationToken ct = default)
    {
        page = Math.Max(1, page);
        pageSize = Math.Clamp(pageSize, 5, 200);

        // monta a query string sem mandar q= vazio (evita 400 em alguns backends)
        var url =
            $"api/lookup/managers?onlyActive={onlyActive.ToString().ToLowerInvariant()}&page={page}&pageSize={pageSize}"
            + (string.IsNullOrWhiteSpace(q) ? "" : $"&q={Uri.EscapeDataString(q.Trim())}");

        using var req = new HttpRequestMessage(HttpMethod.Get, url);
        req.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        req.Headers.TryAddWithoutValidation(TenantHeaderName, TenantId);

        using var res = await _http.SendAsync(req, ct);

        if (!res.IsSuccessStatusCode)
        {
            var body = await res.Content.ReadAsStringAsync(ct);
            throw new HttpRequestException(
                $"GET {url} falhou: {(int)res.StatusCode} {res.ReasonPhrase}. Body: {body}");
        }

        var resp = await res.Content.ReadFromJsonAsync<LookupResponse<GestorLookupItem>>(cancellationToken: ct);

        return resp ?? new LookupResponse<GestorLookupItem>
        {
            Items = Array.Empty<GestorLookupItem>(),
            Total = 0,
            HasMore = false
        };
    }
}

// DTOs para o WEB (pode compartilhar com API se preferir)
public sealed class LookupResponse<T>
{
    public required IReadOnlyList<T> Items { get; init; }
    public required int Total { get; init; }
    public required bool HasMore { get; init; }
}

public sealed class GestorLookupItem
{
    public required Guid Id { get; init; }
    public required string Nome { get; init; }

    public string? Email { get; init; }
    public string? Cargo { get; init; }
    public string? Area { get; init; }
    public string? Unidade { get; init; }

    public string? Status { get; init; }
    public string? Telefone { get; init; }
    public int Headcount { get; init; }
    public string? Observacao { get; init; }

    public DateTimeOffset? CreatedAt { get; init; }
    public DateTimeOffset? UpdatedAt { get; init; }
}
