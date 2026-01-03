using RhPortal.Api.Contracts.Managers;

namespace RhPortal.Api.Application.Managers.Handlers;

public interface IUpdateManagerHandler
{
    Task<ManagerResponse?> HandleAsync(Guid id, ManagerUpdateRequest request, CancellationToken ct);
}

public sealed class UpdateManagerHandler : IUpdateManagerHandler
{
    private readonly Application.Managers.IManagerService _service;
    public UpdateManagerHandler(Application.Managers.IManagerService service) => _service = service;

    public Task<ManagerResponse?> HandleAsync(Guid id, ManagerUpdateRequest request, CancellationToken ct)
        => _service.UpdateAsync(id, request, ct);
}
