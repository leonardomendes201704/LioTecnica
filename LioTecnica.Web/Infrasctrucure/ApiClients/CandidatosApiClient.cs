using System.Text;
using System.Text.Json;

namespace LioTecnica.Web.Infrastructure.ApiClients;

public sealed class CandidatosApiClient
{
    private readonly HttpClient _http;

    private static readonly JsonSerializerOptions JsonOpts = new(JsonSerializerDefaults.Web);

    public CandidatosApiClient(HttpClient http) => _http = http;

    public Task<ApiRawResponse> GetCandidatosRawAsync(string tenantId, CancellationToken ct)
    {
        var req = BuildRequest(HttpMethod.Get, "api/candidatos", tenantId);
        return SendAsync(req, ct);
    }

    public Task<ApiRawResponse> GetCandidatoByIdRawAsync(string tenantId, Guid id, CancellationToken ct)
    {
        var req = BuildRequest(HttpMethod.Get, $"api/candidatos/{id}", tenantId);
        return SendAsync(req, ct);
    }

    public Task<ApiRawResponse> CreateRawAsync(string tenantId, JsonElement payload, CancellationToken ct)
    {
        var json = JsonSerializer.Serialize(payload, JsonOpts);
        var req = BuildRequest(HttpMethod.Post, "api/candidatos", tenantId, jsonBody: json);
        return SendAsync(req, ct);
    }

    public Task<ApiRawResponse> UpdateRawAsync(string tenantId, Guid id, JsonElement payload, CancellationToken ct)
    {
        var json = JsonSerializer.Serialize(payload, JsonOpts);
        var req = BuildRequest(HttpMethod.Put, $"api/candidatos/{id}", tenantId, jsonBody: json);
        return SendAsync(req, ct);
    }

    public Task<ApiRawResponse> DeleteRawAsync(string tenantId, Guid id, CancellationToken ct)
    {
        var req = BuildRequest(HttpMethod.Delete, $"api/candidatos/{id}", tenantId);
        return SendAsync(req, ct);
    }

    private HttpRequestMessage BuildRequest(HttpMethod method, string url, string tenantId, string? jsonBody = null)
    {
        var req = new HttpRequestMessage(method, url);
        req.Headers.TryAddWithoutValidation("X-Tenant-Id", tenantId);
        req.Headers.TryAddWithoutValidation("Accept", "application/json");

        if (jsonBody != null)
            req.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

        return req;
    }

    private async Task<ApiRawResponse> SendAsync(HttpRequestMessage req, CancellationToken ct)
    {
        using var res = await _http.SendAsync(req, ct);
        var content = await res.Content.ReadAsStringAsync(ct);
        return new ApiRawResponse(res.StatusCode, content);
    }
}
