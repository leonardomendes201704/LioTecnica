namespace RhPortal.Api.Infrastructure.Tenancy;

public interface ITenantContext
{
    string TenantId { get; }
    void SetTenantId(string tenantId);
}

public sealed class TenantContext : ITenantContext
{
    public string TenantId { get; private set; } = string.Empty;

    public void SetTenantId(string tenantId)
    {
        var value = tenantId?.Trim();
        if (string.IsNullOrWhiteSpace(value))
            throw new InvalidOperationException("Tenant identifier is required.");

        TenantId = value;
    }
}
