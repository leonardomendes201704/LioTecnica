namespace RhPortal.Api.Contracts.Areas;

public sealed record AreaResponse(Guid Id, string Code, string Name, bool IsActive);
