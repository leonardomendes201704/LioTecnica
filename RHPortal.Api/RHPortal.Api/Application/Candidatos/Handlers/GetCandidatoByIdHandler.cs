using RhPortal.Api.Contracts.Candidatos;

namespace RhPortal.Api.Application.Candidatos.Handlers;

public interface IGetCandidatoByIdHandler
{
    Task<CandidatoResponse?> HandleAsync(Guid id, CancellationToken ct);
}

public sealed class GetCandidatoByIdHandler : IGetCandidatoByIdHandler
{
    private readonly ICandidatoService _service;

    public GetCandidatoByIdHandler(ICandidatoService service) => _service = service;

    public Task<CandidatoResponse?> HandleAsync(Guid id, CancellationToken ct)
        => _service.GetByIdAsync(id, ct);
}
