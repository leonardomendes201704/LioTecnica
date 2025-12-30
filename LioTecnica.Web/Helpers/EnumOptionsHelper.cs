using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using LioTecnica.Web.Enums;
using LioTecnica.Web.ViewModels.Enums;

namespace LioTecnica.Web.Helpers;

public static class EnumOptionsHelper
{
    public static string GetCode<TEnum>(TEnum value) where TEnum : struct, Enum
    {
        var display = GetDisplay(value);
        if (display?.ShortName is not null)
        {
            return display.ShortName;
        }

        return value.ToString();
    }

    public static string GetText<TEnum>(TEnum value) where TEnum : struct, Enum
    {
        var display = GetDisplay(value);
        return display?.Name ?? value.ToString();
    }

    public static IReadOnlyList<EnumOption> GetOptions<TEnum>() where TEnum : struct, Enum
    {
        return Enum.GetValues<TEnum>()
            .Select(value => new EnumOption(GetCode(value), GetText(value)))
            .ToList();
    }

    public static EnumBundle BuildBundle()
    {
        return new EnumBundle
        {
            SelectPlaceholder = GetOptions<SelectPlaceholder>(),
            CandidatoStatus = GetOptions<CandidatoStatus>(),
            CandidatoStatusFilter = GetOptions<CandidatoStatusFilter>(),
            CandidatoFonte = GetOptions<CandidatoFonte>(),
            VagaStatus = GetOptions<VagaStatus>(),
            VagaStatusFilter = GetOptions<VagaStatusFilter>(),
            VagaArea = GetOptions<VagaArea>(),
            VagaAreaFilter = GetOptions<VagaAreaFilter>(),
            VagaModalidade = GetOptions<VagaModalidade>(),
            VagaSenioridade = GetOptions<VagaSenioridade>(),
            VagaDepartamento = GetOptions<VagaDepartamento>(),
            VagaAreaTime = GetOptions<VagaAreaTime>(),
            VagaTipoContratacao = GetOptions<VagaTipoContratacao>(),
            VagaMotivoAbertura = GetOptions<VagaMotivoAbertura>(),
            VagaOrcamentoAprovado = GetOptions<VagaOrcamentoAprovado>(),
            VagaPrioridade = GetOptions<VagaPrioridade>(),
            VagaRegimeJornada = GetOptions<VagaRegimeJornada>(),
            VagaEscalaTrabalho = GetOptions<VagaEscalaTrabalho>(),
            VagaMoeda = GetOptions<VagaMoeda>(),
            VagaRemuneracaoPeriodicidade = GetOptions<VagaRemuneracaoPeriodicidade>(),
            VagaBonusTipo = GetOptions<VagaBonusTipo>(),
            VagaBeneficioTipo = GetOptions<VagaBeneficioTipo>(),
            VagaBeneficioRecorrencia = GetOptions<VagaBeneficioRecorrencia>(),
            VagaEscolaridade = GetOptions<VagaEscolaridade>(),
            VagaFormacaoArea = GetOptions<VagaFormacaoArea>(),
            VagaRequisitoNivel = GetOptions<VagaRequisitoNivel>(),
            VagaRequisitoAvaliacao = GetOptions<VagaRequisitoAvaliacao>(),
            VagaEtapaResponsavel = GetOptions<VagaEtapaResponsavel>(),
            VagaEtapaModo = GetOptions<VagaEtapaModo>(),
            VagaPerguntaTipo = GetOptions<VagaPerguntaTipo>(),
            VagaPeso = GetOptions<VagaPeso>(),
            VagaPublicacaoVisibilidade = GetOptions<VagaPublicacaoVisibilidade>(),
            VagaGeneroPreferencia = GetOptions<VagaGeneroPreferencia>(),
            VagaFilter = GetOptions<VagaFilter>(),
            VagaFilterSimple = GetOptions<VagaFilterSimple>(),
            RequisitoCategoria = GetOptions<RequisitoCategoria>(),
            MatchingSort = GetOptions<MatchingSort>(),
            OrigemFilter = GetOptions<OrigemFilter>(),
            OrigemFilterSimple = GetOptions<OrigemFilterSimple>(),
            InboxStatusFilter = GetOptions<InboxStatusFilter>(),
            InboxStatusFilterSimple = GetOptions<InboxStatusFilterSimple>(),
            RelatorioPeriodo = GetOptions<RelatorioPeriodo>(),
            RelatorioFrequencia = GetOptions<RelatorioFrequencia>(),
            UsuarioStatus = GetOptions<UsuarioStatus>(),
            UsuarioStatusFilter = GetOptions<UsuarioStatusFilter>(),
            UsuarioMfaOption = GetOptions<UsuarioMfaOption>(),
            RoleFilter = GetOptions<RoleFilter>(),
            TriagemDecisionAction = GetOptions<TriagemDecisionAction>(),
            TriagemDecisionReason = GetOptions<TriagemDecisionReason>()
        };
    }

    public static string BuildJson()
    {
        return SeedJsonHelper.ToJson(BuildBundle());
    }

    private static DisplayAttribute? GetDisplay<TEnum>(TEnum value) where TEnum : struct, Enum
    {
        var member = typeof(TEnum).GetMember(value.ToString()).FirstOrDefault();
        return member?.GetCustomAttribute<DisplayAttribute>();
    }
}
