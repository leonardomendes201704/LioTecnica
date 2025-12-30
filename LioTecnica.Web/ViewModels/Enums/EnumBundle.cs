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
    public IReadOnlyList<EnumOption> VagaDepartamento { get; init; } = Array.Empty<EnumOption>();
    public IReadOnlyList<EnumOption> VagaAreaTime { get; init; } = Array.Empty<EnumOption>();
    public IReadOnlyList<EnumOption> VagaTipoContratacao { get; init; } = Array.Empty<EnumOption>();
    public IReadOnlyList<EnumOption> VagaMotivoAbertura { get; init; } = Array.Empty<EnumOption>();
    public IReadOnlyList<EnumOption> VagaOrcamentoAprovado { get; init; } = Array.Empty<EnumOption>();
    public IReadOnlyList<EnumOption> VagaPrioridade { get; init; } = Array.Empty<EnumOption>();
    public IReadOnlyList<EnumOption> VagaRegimeJornada { get; init; } = Array.Empty<EnumOption>();
    public IReadOnlyList<EnumOption> VagaEscalaTrabalho { get; init; } = Array.Empty<EnumOption>();
    public IReadOnlyList<EnumOption> VagaMoeda { get; init; } = Array.Empty<EnumOption>();
    public IReadOnlyList<EnumOption> VagaRemuneracaoPeriodicidade { get; init; } = Array.Empty<EnumOption>();
    public IReadOnlyList<EnumOption> VagaBonusTipo { get; init; } = Array.Empty<EnumOption>();
    public IReadOnlyList<EnumOption> VagaBeneficioTipo { get; init; } = Array.Empty<EnumOption>();
    public IReadOnlyList<EnumOption> VagaBeneficioRecorrencia { get; init; } = Array.Empty<EnumOption>();
    public IReadOnlyList<EnumOption> VagaEscolaridade { get; init; } = Array.Empty<EnumOption>();
    public IReadOnlyList<EnumOption> VagaFormacaoArea { get; init; } = Array.Empty<EnumOption>();
    public IReadOnlyList<EnumOption> VagaRequisitoNivel { get; init; } = Array.Empty<EnumOption>();
    public IReadOnlyList<EnumOption> VagaRequisitoAvaliacao { get; init; } = Array.Empty<EnumOption>();
    public IReadOnlyList<EnumOption> VagaEtapaResponsavel { get; init; } = Array.Empty<EnumOption>();
    public IReadOnlyList<EnumOption> VagaEtapaModo { get; init; } = Array.Empty<EnumOption>();
    public IReadOnlyList<EnumOption> VagaPerguntaTipo { get; init; } = Array.Empty<EnumOption>();
    public IReadOnlyList<EnumOption> VagaPeso { get; init; } = Array.Empty<EnumOption>();
    public IReadOnlyList<EnumOption> VagaPublicacaoVisibilidade { get; init; } = Array.Empty<EnumOption>();
    public IReadOnlyList<EnumOption> VagaGeneroPreferencia { get; init; } = Array.Empty<EnumOption>();
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
