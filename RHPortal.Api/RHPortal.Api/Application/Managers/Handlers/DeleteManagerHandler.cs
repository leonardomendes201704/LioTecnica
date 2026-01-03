namespace RhPortal.Api.Application.Managers.Handlers;

public interface IDeleteManagerHandler
{
    Task<bool> HandleAsync(Guid id, CancellationToken ct);
}

public sealed class DeleteManagerHandler : IDeleteManagerHandler
{
    private readonly Application.Managers.IManagerService _service;
    public DeleteManagerHandler(Application.Managers.IManagerService service) => _service = service;

    public Task<bool> HandleAsync(Guid id, CancellationToken ct)
        => _service.DeleteAsync(id, ct);
}
