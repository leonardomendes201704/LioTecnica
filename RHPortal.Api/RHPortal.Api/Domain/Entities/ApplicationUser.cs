using Microsoft.AspNetCore.Identity;

namespace RhPortal.Api.Domain.Entities;

public sealed class ApplicationUser : IdentityUser<Guid>, ITenantEntity
{
    public string TenantId { get; set; } = default!;
    public string FullName { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public DateTimeOffset CreatedAtUtc { get; set; }
    public DateTimeOffset UpdatedAtUtc { get; set; }
}
