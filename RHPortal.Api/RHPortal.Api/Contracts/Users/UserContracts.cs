using System.ComponentModel.DataAnnotations;

namespace RhPortal.Api.Contracts.Users;

public sealed record RoleInfoResponse(
    Guid Id,
    string Name
);

public sealed record UserListItemResponse(
    Guid Id,
    string FullName,
    string Email,
    bool IsActive,
    IReadOnlyList<string> Roles
);

public sealed record UserResponse(
    Guid Id,
    string FullName,
    string Email,
    bool IsActive,
    IReadOnlyList<RoleInfoResponse> Roles,
    DateTimeOffset CreatedAtUtc,
    DateTimeOffset UpdatedAtUtc
);

public sealed record UserCreateRequest(
    [Required, EmailAddress, MaxLength(180)] string Email,
    [Required, MaxLength(200)] string FullName,
    [Required, MinLength(8), MaxLength(120)] string Password,
    bool IsActive,
    IReadOnlyList<Guid> RoleIds
);

public sealed record UserUpdateRequest(
    [Required, EmailAddress, MaxLength(180)] string Email,
    [Required, MaxLength(200)] string FullName,
    bool IsActive
);

public sealed record UserStatusUpdateRequest(
    [Required] bool IsActive
);

public sealed record UserRolesUpdateRequest(
    [Required] IReadOnlyList<Guid> RoleIds
);
