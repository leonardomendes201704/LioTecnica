using RhPortal.Api.Domain.Enums;

namespace RhPortal.Api.Contracts.Managers;

public sealed record ManagerGridRowResponse(
    Guid Id,
    string Name,
    string Email,
    string? Phone,
    ManagerStatus Status,
    Guid UnitId,
    string UnitName,
    Guid AreaId,
    string AreaName,
    Guid JobPositionId,
    string JobPositionName
);
