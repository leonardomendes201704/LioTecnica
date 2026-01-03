using RhPortal.Api.Contracts.Common;
using RhPortal.Api.Contracts.Managers;

namespace RhPortal.Api.Application.Managers.Handlers;

public interface IListManagersHandler
{
    Task<PagedResult<ManagerGridRowResponse>> HandleAsync(ManagerListQuery query, CancellationToken ct);
}

public sealed class ListManagersHandler : IListManagersHandler
{
    private readonly Application.Managers.IManagerService _service;
    public ListManagersHandler(Application.Managers.IManagerService service) => _service = service;

    public Task<PagedResult<ManagerGridRowResponse>> HandleAsync(ManagerListQuery query, CancellationToken ct)
        => _service.ListGridAsync(query, ct);
}
