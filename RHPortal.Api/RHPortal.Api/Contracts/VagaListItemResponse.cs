using RHPortal.Api.Domain.Enums;

namespace RhPortal.Api.Controllers;

public sealed record VagaListItemResponse(
    Guid Id,
    string? Codigo,
    string Titulo,
    VagaStatus Status,

    Guid AreaId,
    string? AreaCode,
    string? AreaName,

    Guid DepartmentId,
    string? DepartmentCode,
    string? DepartmentName,

    VagaModalidade? Modalidade,
    VagaSenioridade? Senioridade,
    int QuantidadeVagas,
    int MatchMinimoPercentual,

    bool Confidencial,
    bool Urgente,
    bool AceitaPcd,

    DateOnly? DataInicio,
    DateOnly? DataEncerramento,

    string? Cidade,
    string? Uf,

    // ✅ agregados
    int RequisitosTotal,
    int RequisitosObrigatorios,

    DateTimeOffset CreatedAtUtc,
    DateTimeOffset UpdatedAtUtc
);
