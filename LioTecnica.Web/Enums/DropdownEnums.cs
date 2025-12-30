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
    Fechada,
    [Display(Name = "Rascunho", ShortName = "rascunho")]
    Rascunho,
    [Display(Name = "Em triagem", ShortName = "triagem")]
    EmTriagem,
    [Display(Name = "Em entrevistas", ShortName = "entrevistas")]
    EmEntrevistas,
    [Display(Name = "Em oferta", ShortName = "oferta")]
    EmOferta,
    [Display(Name = "Congelada", ShortName = "congelada")]
    Congelada
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
    Gestao,
    [Display(Name = "Estagio", ShortName = "Estagio")]
    Estagio,
    [Display(Name = "Coordenacao", ShortName = "Coordenacao")]
    Coordenacao,
    [Display(Name = "Gerencia", ShortName = "Gerencia")]
    Gerencia,
    [Display(Name = "Diretoria", ShortName = "Diretoria")]
    Diretoria
}

public enum VagaDepartamento
{
    [Display(Name = "RH", ShortName = "rh")]
    Rh,
    [Display(Name = "TI", ShortName = "ti")]
    Ti,
    [Display(Name = "Financeiro", ShortName = "financeiro")]
    Financeiro,
    [Display(Name = "Comercial", ShortName = "comercial")]
    Comercial,
    [Display(Name = "Marketing", ShortName = "marketing")]
    Marketing,
    [Display(Name = "Operacoes", ShortName = "operacoes")]
    Operacoes,
    [Display(Name = "Juridico", ShortName = "juridico")]
    Juridico,
    [Display(Name = "Logistica", ShortName = "logistica")]
    Logistica,
    [Display(Name = "Produto", ShortName = "produto")]
    Produto
}

public enum VagaAreaTime
{
    [Display(Name = "Talent Acquisition", ShortName = "talent_acquisition")]
    TalentAcquisition,
    [Display(Name = "People Analytics", ShortName = "people_analytics")]
    PeopleAnalytics,
    [Display(Name = "Folha e Beneficios", ShortName = "folha_beneficios")]
    FolhaBeneficios,
    [Display(Name = "Infraestrutura", ShortName = "infraestrutura")]
    Infraestrutura,
    [Display(Name = "Desenvolvimento", ShortName = "desenvolvimento")]
    Desenvolvimento,
    [Display(Name = "Suporte", ShortName = "suporte")]
    Suporte,
    [Display(Name = "Contas a Receber", ShortName = "contas_receber")]
    ContasReceber,
    [Display(Name = "Vendas Enterprise", ShortName = "vendas_enterprise")]
    VendasEnterprise,
    [Display(Name = "Operacao de Armazem", ShortName = "operacao_armazem")]
    OperacaoArmazem
}

public enum VagaTipoContratacao
{
    [Display(Name = "CLT", ShortName = "clt")]
    Clt,
    [Display(Name = "PJ", ShortName = "pj")]
    Pj,
    [Display(Name = "Estagio", ShortName = "estagio")]
    Estagio,
    [Display(Name = "Temporario", ShortName = "temporario")]
    Temporario,
    [Display(Name = "Aprendiz", ShortName = "aprendiz")]
    Aprendiz,
    [Display(Name = "Cooperado", ShortName = "cooperado")]
    Cooperado,
    [Display(Name = "Freelancer", ShortName = "freelancer")]
    Freelancer
}

public enum VagaMotivoAbertura
{
    [Display(Name = "Reposicao", ShortName = "reposicao")]
    Reposicao,
    [Display(Name = "Expansao", ShortName = "expansao")]
    Expansao,
    [Display(Name = "Projeto", ShortName = "projeto")]
    Projeto,
    [Display(Name = "Substituicao", ShortName = "substituicao")]
    Substituicao,
    [Display(Name = "Backfill", ShortName = "backfill")]
    Backfill
}

public enum VagaOrcamentoAprovado
{
    [Display(Name = "Sim", ShortName = "sim")]
    Sim,
    [Display(Name = "Nao", ShortName = "nao")]
    Nao,
    [Display(Name = "Em aprovacao", ShortName = "em_aprovacao")]
    EmAprovacao
}

public enum VagaPrioridade
{
    [Display(Name = "Baixa", ShortName = "baixa")]
    Baixa,
    [Display(Name = "Media", ShortName = "media")]
    Media,
    [Display(Name = "Alta", ShortName = "alta")]
    Alta,
    [Display(Name = "Critica", ShortName = "critica")]
    Critica
}

public enum VagaRegimeJornada
{
    [Display(Name = "Integral", ShortName = "integral")]
    Integral,
    [Display(Name = "Meio periodo", ShortName = "meio_periodo")]
    MeioPeriodo,
    [Display(Name = "Banco de horas", ShortName = "banco_horas")]
    BancoHoras,
    [Display(Name = "Escala", ShortName = "escala")]
    Escala,
    [Display(Name = "Por projeto", ShortName = "projeto")]
    PorProjeto
}

public enum VagaEscalaTrabalho
{
    [Display(Name = "5x2", ShortName = "5x2")]
    Escala5x2,
    [Display(Name = "6x1", ShortName = "6x1")]
    Escala6x1,
    [Display(Name = "12x36", ShortName = "12x36")]
    Escala12x36,
    [Display(Name = "4x3", ShortName = "4x3")]
    Escala4x3,
    [Display(Name = "Turnos", ShortName = "turnos")]
    Turnos
}

public enum VagaMoeda
{
    [Display(Name = "BRL", ShortName = "BRL")]
    Brl,
    [Display(Name = "USD", ShortName = "USD")]
    Usd,
    [Display(Name = "EUR", ShortName = "EUR")]
    Eur
}

public enum VagaRemuneracaoPeriodicidade
{
    [Display(Name = "Mensal", ShortName = "mensal")]
    Mensal,
    [Display(Name = "Hora", ShortName = "hora")]
    Hora,
    [Display(Name = "Diaria", ShortName = "diaria")]
    Diaria,
    [Display(Name = "Projeto", ShortName = "projeto")]
    Projeto
}

public enum VagaBonusTipo
{
    [Display(Name = "PLR", ShortName = "plr")]
    Plr,
    [Display(Name = "Bonus anual", ShortName = "bonus_anual")]
    BonusAnual,
    [Display(Name = "Comissao", ShortName = "comissao")]
    Comissao,
    [Display(Name = "Premiacao", ShortName = "premiacao")]
    Premiacao
}

public enum VagaBeneficioTipo
{
    [Display(Name = "Vale Alimentacao", ShortName = "vale_alimentacao")]
    ValeAlimentacao,
    [Display(Name = "Vale Refeicao", ShortName = "vale_refeicao")]
    ValeRefeicao,
    [Display(Name = "Plano de Saude", ShortName = "plano_saude")]
    PlanoSaude,
    [Display(Name = "Plano Odonto", ShortName = "plano_odonto")]
    PlanoOdonto,
    [Display(Name = "Seguro de Vida", ShortName = "seguro_vida")]
    SeguroVida,
    [Display(Name = "Vale Transporte", ShortName = "vale_transporte")]
    ValeTransporte,
    [Display(Name = "Ajuda de custo", ShortName = "ajuda_custo")]
    AjudaCusto,
    [Display(Name = "Gympass/Wellhub", ShortName = "gympass")]
    Gympass,
    [Display(Name = "Home office", ShortName = "home_office")]
    HomeOffice,
    [Display(Name = "PLR", ShortName = "plr")]
    Plr,
    [Display(Name = "Bonus", ShortName = "bonus")]
    Bonus,
    [Display(Name = "Day off", ShortName = "day_off")]
    DayOff,
    [Display(Name = "Bolsa de estudos", ShortName = "bolsa_estudos")]
    BolsaEstudos
}

public enum VagaBeneficioRecorrencia
{
    [Display(Name = "Mensal", ShortName = "mensal")]
    Mensal,
    [Display(Name = "Anual", ShortName = "anual")]
    Anual,
    [Display(Name = "Por uso", ShortName = "por_uso")]
    PorUso,
    [Display(Name = "Reembolso", ShortName = "reembolso")]
    Reembolso,
    [Display(Name = "Conforme politica", ShortName = "conforme_politica")]
    ConformePolitica
}

public enum VagaEscolaridade
{
    [Display(Name = "Ensino medio", ShortName = "ensino_medio")]
    EnsinoMedio,
    [Display(Name = "Tecnico", ShortName = "tecnico")]
    Tecnico,
    [Display(Name = "Graduacao", ShortName = "graduacao")]
    Graduacao,
    [Display(Name = "Pos-graduacao", ShortName = "pos_graduacao")]
    PosGraduacao,
    [Display(Name = "Mestrado", ShortName = "mestrado")]
    Mestrado,
    [Display(Name = "Doutorado", ShortName = "doutorado")]
    Doutorado
}

public enum VagaFormacaoArea
{
    [Display(Name = "Administracao", ShortName = "administracao")]
    Administracao,
    [Display(Name = "Psicologia", ShortName = "psicologia")]
    Psicologia,
    [Display(Name = "Recursos Humanos", ShortName = "recursos_humanos")]
    RecursosHumanos,
    [Display(Name = "Engenharia", ShortName = "engenharia")]
    Engenharia,
    [Display(Name = "Sistemas de Informacao", ShortName = "sistemas_informacao")]
    SistemasInformacao,
    [Display(Name = "Contabilidade", ShortName = "contabilidade")]
    Contabilidade,
    [Display(Name = "Direito", ShortName = "direito")]
    Direito
}

public enum VagaRequisitoNivel
{
    [Display(Name = "Basico", ShortName = "basico")]
    Basico,
    [Display(Name = "Intermediario", ShortName = "intermediario")]
    Intermediario,
    [Display(Name = "Avancado", ShortName = "avancado")]
    Avancado,
    [Display(Name = "Especialista", ShortName = "especialista")]
    Especialista
}

public enum VagaRequisitoAvaliacao
{
    [Display(Name = "Entrevista", ShortName = "entrevista")]
    Entrevista,
    [Display(Name = "Case", ShortName = "case")]
    Case,
    [Display(Name = "Teste tecnico", ShortName = "teste_tecnico")]
    TesteTecnico,
    [Display(Name = "Dinamica", ShortName = "dinamica")]
    Dinamica,
    [Display(Name = "Prova", ShortName = "prova")]
    Prova,
    [Display(Name = "Portfolio", ShortName = "portfolio")]
    Portfolio,
    [Display(Name = "Referencias", ShortName = "referencias")]
    Referencias
}

public enum VagaEtapaResponsavel
{
    [Display(Name = "Recrutador", ShortName = "recrutador")]
    Recrutador,
    [Display(Name = "Gestor", ShortName = "gestor")]
    Gestor,
    [Display(Name = "Tech Lead", ShortName = "tech_lead")]
    TechLead,
    [Display(Name = "RH BP", ShortName = "rh_bp")]
    RhBp,
    [Display(Name = "Diretoria", ShortName = "diretoria")]
    Diretoria,
    [Display(Name = "Banca", ShortName = "banca")]
    Banca
}

public enum VagaEtapaModo
{
    [Display(Name = "Online", ShortName = "online")]
    Online,
    [Display(Name = "Presencial", ShortName = "presencial")]
    Presencial,
    [Display(Name = "Telefone", ShortName = "telefone")]
    Telefone,
    [Display(Name = "Assincrono", ShortName = "assincrono")]
    Assincrono
}

public enum VagaPerguntaTipo
{
    [Display(Name = "Sim/Nao", ShortName = "sim_nao")]
    SimNao,
    [Display(Name = "Texto", ShortName = "texto")]
    Texto,
    [Display(Name = "Numero", ShortName = "numero")]
    Numero,
    [Display(Name = "Multipla escolha", ShortName = "multipla")]
    MultiplaEscolha
}

public enum VagaPeso
{
    [Display(Name = "1", ShortName = "1")]
    Peso1,
    [Display(Name = "2", ShortName = "2")]
    Peso2,
    [Display(Name = "3", ShortName = "3")]
    Peso3,
    [Display(Name = "4", ShortName = "4")]
    Peso4,
    [Display(Name = "5", ShortName = "5")]
    Peso5
}

public enum VagaPublicacaoVisibilidade
{
    [Display(Name = "Interno", ShortName = "interno")]
    Interno,
    [Display(Name = "Externo", ShortName = "externo")]
    Externo,
    [Display(Name = "Interno + Externo", ShortName = "interno_externo")]
    InternoExterno,
    [Display(Name = "Somente indicacao", ShortName = "indicacao")]
    SomenteIndicacao
}

public enum VagaGeneroPreferencia
{
    [Display(Name = "Todas", ShortName = "todas")]
    Todas,
    [Display(Name = "Mulheres", ShortName = "mulheres")]
    Mulheres,
    [Display(Name = "Homens", ShortName = "homens")]
    Homens,
    [Display(Name = "Nao binario", ShortName = "nao_binario")]
    NaoBinario,
    [Display(Name = "LGBTQ+", ShortName = "lgbtq")]
    Lgbtq,
    [Display(Name = "Trans", ShortName = "trans")]
    Trans,
    [Display(Name = "Nao informar", ShortName = "nao_informar")]
    NaoInformar
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
