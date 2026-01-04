using RhPortal.Api.Contracts.Candidatos;

namespace RhPortal.Api.Application.Candidatos.Handlers;

public interface IUpdateCandidatoHandler
{
    Task<CandidatoResponse?> HandleAsync(Guid id, CandidatoUpdateRequest request, CancellationToken ct);
}

public sealed class UpdateCandidatoHandler : IUpdateCandidatoHandler
{
    private readonly ICandidatoService _service;

    public UpdateCandidatoHandler(ICandidatoService service) => _service = service;

    public Task<CandidatoResponse?> HandleAsync(Guid id, CandidatoUpdateRequest request, CancellationToken ct)
        => _service.UpdateAsync(id, request, ct);
}
