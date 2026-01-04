using RhPortal.Api.Contracts.Candidatos;

namespace RhPortal.Api.Application.Candidatos.Handlers;

public interface ICreateCandidatoHandler
{
    Task<CandidatoResponse> HandleAsync(CandidatoCreateRequest request, CancellationToken ct);
}

public sealed class CreateCandidatoHandler : ICreateCandidatoHandler
{
    private readonly ICandidatoService _service;

    public CreateCandidatoHandler(ICandidatoService service) => _service = service;

    public Task<CandidatoResponse> HandleAsync(CandidatoCreateRequest request, CancellationToken ct)
        => _service.CreateAsync(request, ct);
}
