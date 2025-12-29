using System;
using System.Collections.Generic;

namespace LioTecnica.Web.ViewModels.Enums;

public sealed class EnumBundle
{
    public IReadOnlyList<EnumOption> SelectPlaceholder { get; init; } = Array.Empty<EnumOption>();
    public IReadOnlyList<EnumOption> CandidatoStatus { get; init; } = Array.Empty<EnumOption>();
    public IReadOnlyList<EnumOption> CandidatoStatusFilter { get; init; } = Array.Empty<EnumOption>();
    public IReadOnlyList<EnumOption> CandidatoFonte { get; init; } = Array.Empty<EnumOption>();

    public IReadOnlyList<EnumOption> VagaStatus { get; init; } = Array.Empty<EnumOption>();
    public IReadOnlyList<EnumOption> VagaStatusFilter { get; init; } = Array.Empty<EnumOption>();
    public IReadOnlyList<EnumOption> VagaArea { get; init; } = Array.Empty<EnumOption>();
    public IReadOnlyList<EnumOption> VagaAreaFilter { get; init; } = Array.Empty<EnumOption>();
    public IReadOnlyList<EnumOption> VagaModalidade { get; init; } = Array.Empty<EnumOption>();
    public IReadOnlyList<EnumOption> VagaSenioridade { get; init; } = Array.Empty<EnumOption>();
    public IReadOnlyList<EnumOption> VagaFilter { get; init; } = Array.Empty<EnumOption>();
    public IReadOnlyList<EnumOption> VagaFilterSimple { get; init; } = Array.Empty<EnumOption>();

    public IReadOnlyList<EnumOption> RequisitoCategoria { get; init; } = Array.Empty<EnumOption>();
    public IReadOnlyList<EnumOption> MatchingSort { get; init; } = Array.Empty<EnumOption>();

    public IReadOnlyList<EnumOption> OrigemFilter { get; init; } = Array.Empty<EnumOption>();
    public IReadOnlyList<EnumOption> OrigemFilterSimple { get; init; } = Array.Empty<EnumOption>();
    public IReadOnlyList<EnumOption> InboxStatusFilter { get; init; } = Array.Empty<EnumOption>();
    public IReadOnlyList<EnumOption> InboxStatusFilterSimple { get; init; } = Array.Empty<EnumOption>();

    public IReadOnlyList<EnumOption> RelatorioPeriodo { get; init; } = Array.Empty<EnumOption>();
    public IReadOnlyList<EnumOption> RelatorioFrequencia { get; init; } = Array.Empty<EnumOption>();

    public IReadOnlyList<EnumOption> UsuarioStatus { get; init; } = Array.Empty<EnumOption>();
    public IReadOnlyList<EnumOption> UsuarioStatusFilter { get; init; } = Array.Empty<EnumOption>();
    public IReadOnlyList<EnumOption> UsuarioMfaOption { get; init; } = Array.Empty<EnumOption>();
    public IReadOnlyList<EnumOption> RoleFilter { get; init; } = Array.Empty<EnumOption>();

    public IReadOnlyList<EnumOption> TriagemDecisionAction { get; init; } = Array.Empty<EnumOption>();
    public IReadOnlyList<EnumOption> TriagemDecisionReason { get; init; } = Array.Empty<EnumOption>();
}
