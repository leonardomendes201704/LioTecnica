using System.Net.Http.Headers;
using System.Text.Json;

namespace RhPortal.Web.Infrastructure.ApiClients;

public sealed class UnitsApiClient
{
    private readonly HttpClient _http;

    private static readonly JsonSerializerOptions JsonOpts = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public UnitsApiClient(HttpClient http)
    {
        _http = http;
    }

    public async Task<UnitsPagedResponse> GetUnitsAsync(string tenantId, CancellationToken ct)
    {
        using var req = new HttpRequestMessage(HttpMethod.Get, "/api/units");
        req.Headers.TryAddWithoutValidation("X-Tenant-Id", tenantId);
        req.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        using var resp = await _http.SendAsync(req, ct);
        resp.EnsureSuccessStatusCode();

        var json = await resp.Content.ReadAsStringAsync(ct);
        var data = JsonSerializer.Deserialize<UnitsPagedResponse>(json, JsonOpts);

        return data ?? new UnitsPagedResponse();
    }
}
