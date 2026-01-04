using System.Net;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;

namespace LioTecnica.Web.Infrastructure.Security;

public sealed class ApiAuthenticationHandler : DelegatingHandler
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ApiAuthenticationHandler(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext?.User?.Identity?.IsAuthenticated == true)
        {
            var tenantId = httpContext.User.FindFirst("tenant")?.Value;
            if (!string.IsNullOrWhiteSpace(tenantId) && !request.Headers.Contains("X-Tenant-Id"))
                request.Headers.TryAddWithoutValidation("X-Tenant-Id", tenantId);

            var token = httpContext.User.FindFirst("access_token")?.Value;
            if (!string.IsNullOrWhiteSpace(token) && request.Headers.Authorization is null)
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        var response = await base.SendAsync(request, cancellationToken);

        if (response.StatusCode == HttpStatusCode.Unauthorized && httpContext is not null)
        {
            httpContext.Items["ApiUnauthorized"] = true;
        }

        return response;
    }
}
