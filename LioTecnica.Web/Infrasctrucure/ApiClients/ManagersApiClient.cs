using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RhPortal.Web.Infrastructure.ApiClients;

public sealed class ManagersApiClient
{
    private readonly HttpClient _http;

    private static readonly JsonSerializerOptions JsonOpts = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public ManagersApiClient(HttpClient http) => _http = http;

    public async Task<ManagersPagedResponse> GetManagersAsync(
        string tenantId,
        Guid unitId,
        int page,
        int pageSize,
        CancellationToken ct)
    {
        var url = $"/api/managers?unitId={unitId:D}&page={page}&pageSize={pageSize}";

        using var req = new HttpRequestMessage(HttpMethod.Get, url);
        req.Headers.TryAddWithoutValidation("X-Tenant-Id", tenantId);
        req.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        using var resp = await _http.SendAsync(req, ct);
        resp.EnsureSuccessStatusCode();

        var json = await resp.Content.ReadAsStringAsync(ct);
        var data = JsonSerializer.Deserialize<ManagersPagedResponse>(json, JsonOpts);

        return data ?? new ManagersPagedResponse();
    }
}

public sealed class ManagersPagedResponse
{
    [JsonPropertyName("items")]
    public List<ManagerApiItem> Items { get; set; } = new();

    [JsonPropertyName("page")]
    public int Page { get; set; }

    [JsonPropertyName("pageSize")]
    public int PageSize { get; set; }

    [JsonPropertyName("totalItems")]
    public int TotalItems { get; set; }

    [JsonPropertyName("totalPages")]
    public int TotalPages { get; set; }
}

public sealed class ManagerApiItem
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [JsonPropertyName("jobTitle")]
    public string? JobTitle { get; set; }

    [JsonPropertyName("area")]
    public string? Area { get; set; }

    [JsonPropertyName("unitName")]
    public string? UnitName { get; set; }

    [JsonPropertyName("status")]
    public string? Status { get; set; } // "Active"/"Inactive"

    [JsonPropertyName("headcount")]
    public int Headcount { get; set; }
}
