using System.ComponentModel.DataAnnotations;

namespace LioTecnica.Web.Enums;

public enum SelectPlaceholder
{
    [Display(Name = "Selecione...", ShortName = "")]
    Select
}

public enum CandidatoStatus
{
    [Display(Name = "Novo", ShortName = "novo")]
    Novo,
    [Display(Name = "Em triagem", ShortName = "triagem")]
    Triagem,
    [Display(Name = "Aprovado", ShortName = "aprovado")]
    Aprovado,
    [Display(Name = "Reprovado", ShortName = "reprovado")]
    Reprovado,
    [Display(Name = "Pendente", ShortName = "pendente")]
    Pendente
}

public enum CandidatoStatusFilter
{
    [Display(Name = "Status: todos", ShortName = "all")]
    All,
    [Display(Name = "Novo", ShortName = "novo")]
    Novo,
    [Display(Name = "Em triagem", ShortName = "triagem")]
    Triagem,
    [Display(Name = "Aprovado", ShortName = "aprovado")]
    Aprovado,
    [Display(Name = "Reprovado", ShortName = "reprovado")]
    Reprovado,
    [Display(Name = "Pendente", ShortName = "pendente")]
    Pendente
}

public enum CandidatoFonte
{
    [Display(Name = "Email", ShortName = "Email")]
    Email,
    [Display(Name = "Pasta", ShortName = "Pasta")]
    Pasta,
    [Display(Name = "LinkedIn", ShortName = "LinkedIn")]
    LinkedIn,
    [Display(Name = "Indicacao", ShortName = "Indicacao")]
    Indicacao,
    [Display(Name = "Site", ShortName = "Site")]
    Site
}

public enum VagaStatus
{
    [Display(Name = "Aberta", ShortName = "aberta")]
    Aberta,
    [Display(Name = "Pausada", ShortName = "pausada")]
    Pausada,
    [Display(Name = "Fechada", ShortName = "fechada")]
    Fechada
}

public enum VagaStatusFilter
{
    [Display(Name = "Status: todos", ShortName = "all")]
    All,
    [Display(Name = "Aberta", ShortName = "aberta")]
    Aberta,
    [Display(Name = "Pausada", ShortName = "pausada")]
    Pausada,
    [Display(Name = "Fechada", ShortName = "fechada")]
    Fechada
}

public enum VagaArea
{
    [Display(Name = "Marketing", ShortName = "Marketing")]
    Marketing,
    [Display(Name = "Qualidade", ShortName = "Qualidade")]
    Qualidade,
    [Display(Name = "Producao", ShortName = "Producao")]
    Producao,
    [Display(Name = "RH", ShortName = "RH")]
    Rh,
    [Display(Name = "TI", ShortName = "TI")]
    Ti,
    [Display(Name = "Compras", ShortName = "Compras")]
    Compras,
    [Display(Name = "Logistica", ShortName = "Logistica")]
    Logistica,
    [Display(Name = "Financeiro", ShortName = "Financeiro")]
    Financeiro
}

public enum VagaAreaFilter
{
    [Display(Name = "Area: todas", ShortName = "all")]
    All
}

public enum VagaModalidade
{
    [Display(Name = "Presencial", ShortName = "Presencial")]
    Presencial,
    [Display(Name = "Hibrido", ShortName = "Hibrido")]
    Hibrido,
    [Display(Name = "Remoto", ShortName = "Remoto")]
    Remoto
}

public enum VagaSenioridade
{
    [Display(Name = "Junior", ShortName = "Junior")]
    Junior,
    [Display(Name = "Pleno", ShortName = "Pleno")]
    Pleno,
    [Display(Name = "Senior", ShortName = "Senior")]
    Senior,
    [Display(Name = "Especialista", ShortName = "Especialista")]
    Especialista,
    [Display(Name = "Gestao", ShortName = "Gestao")]
    Gestao
}

public enum RequisitoCategoria
{
    [Display(Name = "Competencia", ShortName = "Competencia")]
    Competencia,
    [Display(Name = "Experiencia", ShortName = "Experiencia")]
    Experiencia,
    [Display(Name = "Formacao", ShortName = "Formacao")]
    Formacao,
    [Display(Name = "Ferramenta/Tecnologia", ShortName = "Ferramenta/Tecnologia")]
    FerramentaTecnologia,
    [Display(Name = "Idioma", ShortName = "Idioma")]
    Idioma,
    [Display(Name = "Certificacao", ShortName = "Certificacao")]
    Certificacao,
    [Display(Name = "Localidade", ShortName = "Localidade")]
    Localidade,
    [Display(Name = "Outros", ShortName = "Outros")]
    Outros
}

public enum MatchingSort
{
    [Display(Name = "Ordenar: Match (maior -> menor)", ShortName = "score_desc")]
    ScoreDesc,
    [Display(Name = "Ordenar: Match (menor -> maior)", ShortName = "score_asc")]
    ScoreAsc,
    [Display(Name = "Ordenar: Atualizacao (recente)", ShortName = "updated_desc")]
    UpdatedDesc,
    [Display(Name = "Ordenar: Atualizacao (antiga)", ShortName = "updated_asc")]
    UpdatedAsc,
    [Display(Name = "Ordenar: Nome (A-Z)", ShortName = "name_asc")]
    NameAsc
}

public enum OrigemFilter
{
    [Display(Name = "Origem: todas", ShortName = "all")]
    All,
    [Display(Name = "Email", ShortName = "email")]
    Email,
    [Display(Name = "Pasta", ShortName = "pasta")]
    Pasta,
    [Display(Name = "Upload", ShortName = "upload")]
    Upload
}

public enum OrigemFilterSimple
{
    [Display(Name = "Todas", ShortName = "all")]
    All,
    [Display(Name = "Email", ShortName = "email")]
    Email,
    [Display(Name = "Pasta", ShortName = "pasta")]
    Pasta,
    [Display(Name = "Upload", ShortName = "upload")]
    Upload
}

public enum InboxStatusFilter
{
    [Display(Name = "Status: todos", ShortName = "all")]
    All,
    [Display(Name = "Novo", ShortName = "novo")]
    Novo,
    [Display(Name = "Processando", ShortName = "processando")]
    Processando,
    [Display(Name = "Processado", ShortName = "processado")]
    Processado,
    [Display(Name = "Falha", ShortName = "falha")]
    Falha,
    [Display(Name = "Descartado", ShortName = "descartado")]
    Descartado
}

public enum InboxStatusFilterSimple
{
    [Display(Name = "Todos", ShortName = "all")]
    All,
    [Display(Name = "Novo", ShortName = "novo")]
    Novo,
    [Display(Name = "Processando", ShortName = "processando")]
    Processando,
    [Display(Name = "Processado", ShortName = "processado")]
    Processado,
    [Display(Name = "Falha", ShortName = "falha")]
    Falha,
    [Display(Name = "Descartado", ShortName = "descartado")]
    Descartado
}

public enum RelatorioPeriodo
{
    [Display(Name = "Ultimos 7 dias", ShortName = "7d")]
    Last7Days,
    [Display(Name = "Ultimos 30 dias", ShortName = "30d")]
    Last30Days,
    [Display(Name = "Ultimos 90 dias", ShortName = "90d")]
    Last90Days,
    [Display(Name = "Ano atual (YTD)", ShortName = "ytd")]
    YearToDate
}

public enum RelatorioFrequencia
{
    [Display(Name = "Diario", ShortName = "daily")]
    Daily,
    [Display(Name = "Semanal", ShortName = "weekly")]
    Weekly,
    [Display(Name = "Mensal", ShortName = "monthly")]
    Monthly
}

public enum UsuarioStatus
{
    [Display(Name = "Ativo", ShortName = "active")]
    Active,
    [Display(Name = "Convidado", ShortName = "invited")]
    Invited,
    [Display(Name = "Desativado", ShortName = "disabled")]
    Disabled
}

public enum UsuarioStatusFilter
{
    [Display(Name = "Todos", ShortName = "all")]
    All,
    [Display(Name = "Ativo", ShortName = "active")]
    Active,
    [Display(Name = "Convidado", ShortName = "invited")]
    Invited,
    [Display(Name = "Desativado", ShortName = "disabled")]
    Disabled
}

public enum UsuarioMfaOption
{
    [Display(Name = "Desabilitado", ShortName = "false")]
    Disabled,
    [Display(Name = "Habilitado", ShortName = "true")]
    Enabled
}

public enum RoleFilter
{
    [Display(Name = "Todos", ShortName = "all")]
    All
}

public enum VagaFilter
{
    [Display(Name = "Vaga: todas", ShortName = "all")]
    All
}

public enum VagaFilterSimple
{
    [Display(Name = "Todas", ShortName = "all")]
    All
}

public enum TriagemDecisionAction
{
    [Display(Name = "Aprovar", ShortName = "aprovado")]
    Aprovado,
    [Display(Name = "Marcar como Pendente", ShortName = "pendente")]
    Pendente,
    [Display(Name = "Reprovar", ShortName = "reprovado")]
    Reprovado,
    [Display(Name = "Manter em Triagem", ShortName = "triagem")]
    Triagem
}

public enum TriagemDecisionReason
{
    [Display(Name = "(opcional)", ShortName = "")]
    None,
    [Display(Name = "Faltou requisito obrigatorio", ShortName = "missing_mandatory")]
    MissingMandatory,
    [Display(Name = "Match abaixo do minimo", ShortName = "below_threshold")]
    BelowThreshold,
    [Display(Name = "Perfil aderente", ShortName = "profile_fit")]
    ProfileFit,
    [Display(Name = "Necessita validacao tecnica", ShortName = "needs_validation")]
    NeedsValidation,
    [Display(Name = "Experiencia insuficiente", ShortName = "low_experience")]
    LowExperience,
    [Display(Name = "Localizacao/Disponibilidade", ShortName = "location_availability")]
    LocationAvailability
}
