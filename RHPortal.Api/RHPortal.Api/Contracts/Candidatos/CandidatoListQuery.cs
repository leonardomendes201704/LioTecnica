using RhPortal.Api.Domain.Enums;

namespace RhPortal.Api.Contracts.Candidatos;

public sealed record CandidatoListQuery(
    string? Q,
    CandidatoStatus? Status,
    Guid? VagaId,
    CandidatoFonte? Fonte
);
