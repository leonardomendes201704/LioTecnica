namespace RhPortal.Api.Infrastructure.Tenancy;

public interface ITenantContext
{
    string TenantId { get; }
    void SetTenantId(string tenantId);
}

public sealed class TenantContext : ITenantContext
{
    public string TenantId { get; private set; } = "dev";

    public void SetTenantId(string tenantId)
    {
        TenantId = string.IsNullOrWhiteSpace(tenantId) ? "dev" : tenantId.Trim();
    }
}
