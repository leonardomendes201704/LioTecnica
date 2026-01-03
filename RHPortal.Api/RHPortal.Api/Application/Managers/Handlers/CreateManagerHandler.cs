using RhPortal.Api.Contracts.Managers;

namespace RhPortal.Api.Application.Managers.Handlers;

public interface ICreateManagerHandler
{
    Task<ManagerResponse> HandleAsync(ManagerCreateRequest request, CancellationToken ct);
}

public sealed class CreateManagerHandler : ICreateManagerHandler
{
    private readonly Application.Managers.IManagerService _service;
    public CreateManagerHandler(Application.Managers.IManagerService service) => _service = service;

    public Task<ManagerResponse> HandleAsync(ManagerCreateRequest request, CancellationToken ct)
        => _service.CreateAsync(request, ct);
}
