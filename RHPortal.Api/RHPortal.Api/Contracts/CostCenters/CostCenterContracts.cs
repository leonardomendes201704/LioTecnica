using System.ComponentModel.DataAnnotations;

namespace RhPortal.Api.Contracts.CostCenters;

public sealed record CostCenterCreateRequest(
    [Required, MaxLength(40)] string Code,
    [Required, MaxLength(160)] string Name,
    [MaxLength(1000)] string? Description,
    [MaxLength(120)] string? GroupName,
    [MaxLength(160)] string? UnitName,
    bool IsActive
);

public sealed record CostCenterUpdateRequest(
    [Required, MaxLength(40)] string Code,
    [Required, MaxLength(160)] string Name,
    [MaxLength(1000)] string? Description,
    [MaxLength(120)] string? GroupName,
    [MaxLength(160)] string? UnitName,
    bool IsActive
);

public sealed record CostCenterResponse(
    Guid Id,
    string Code,
    string Name,
    string? Description,
    string? GroupName,
    string? UnitName,
    bool IsActive
);
