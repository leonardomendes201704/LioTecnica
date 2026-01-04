using Microsoft.AspNetCore.Http;

namespace LioTecnica.Web.Infrastructure.Security;

public sealed class PortalTenantContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public PortalTenantContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string TenantId
    {
        get
        {
            var tenantId = _httpContextAccessor.HttpContext?.User?.FindFirst("tenant")?.Value;
            if (string.IsNullOrWhiteSpace(tenantId))
                throw new InvalidOperationException("Tenant identifier is required.");

            return tenantId;
        }
    }
}
