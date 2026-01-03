using RhPortal.Api.Contracts.Managers;

namespace RhPortal.Api.Application.Managers.Handlers;

public interface IGetManagerByIdHandler
{
    Task<ManagerResponse?> HandleAsync(Guid id, CancellationToken ct);
}

public sealed class GetManagerByIdHandler : IGetManagerByIdHandler
{
    private readonly Application.Managers.IManagerService _service;
    public GetManagerByIdHandler(Application.Managers.IManagerService service) => _service = service;

    public Task<ManagerResponse?> HandleAsync(Guid id, CancellationToken ct)
        => _service.GetByIdAsync(id, ct);
}
