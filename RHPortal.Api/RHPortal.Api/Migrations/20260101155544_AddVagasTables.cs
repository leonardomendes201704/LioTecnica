using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RHPortal.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddVagasTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vagas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    TenantId = table.Column<string>(type: "TEXT", maxLength: 64, nullable: false),
                    Codigo = table.Column<string>(type: "TEXT", maxLength: 40, nullable: true),
                    Titulo = table.Column<string>(type: "TEXT", maxLength: 160, nullable: false),
                    Departamento = table.Column<short>(type: "INTEGER", nullable: false),
                    AreaTime = table.Column<short>(type: "INTEGER", nullable: true),
                    Area = table.Column<short>(type: "INTEGER", nullable: false),
                    Modalidade = table.Column<short>(type: "INTEGER", nullable: true),
                    Status = table.Column<short>(type: "INTEGER", nullable: false),
                    Senioridade = table.Column<short>(type: "INTEGER", nullable: true),
                    QuantidadeVagas = table.Column<int>(type: "INTEGER", nullable: false),
                    TipoContratacao = table.Column<short>(type: "INTEGER", nullable: true),
                    MatchMinimoPercentual = table.Column<int>(type: "INTEGER", nullable: false),
                    DescricaoInterna = table.Column<string>(type: "TEXT", nullable: true),
                    CodigoInterno = table.Column<string>(type: "TEXT", maxLength: 40, nullable: true),
                    CodigoCbo = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    MotivoAbertura = table.Column<short>(type: "INTEGER", nullable: true),
                    OrcamentoAprovado = table.Column<short>(type: "INTEGER", nullable: true),
                    GestorRequisitante = table.Column<string>(type: "TEXT", maxLength: 120, nullable: true),
                    RecrutadorResponsavel = table.Column<string>(type: "TEXT", maxLength: 120, nullable: true),
                    Prioridade = table.Column<short>(type: "INTEGER", nullable: true),
                    ResumoPitch = table.Column<string>(type: "TEXT", nullable: true),
                    TagsResponsabilidadesRaw = table.Column<string>(type: "TEXT", nullable: true),
                    TagsKeywordsRaw = table.Column<string>(type: "TEXT", nullable: true),
                    Confidencial = table.Column<bool>(type: "INTEGER", nullable: false),
                    AceitaPcd = table.Column<bool>(type: "INTEGER", nullable: false),
                    Urgente = table.Column<bool>(type: "INTEGER", nullable: false),
                    GeneroPreferencia = table.Column<short>(type: "INTEGER", nullable: true),
                    VagaAfirmativa = table.Column<bool>(type: "INTEGER", nullable: false),
                    LinguagemInclusiva = table.Column<bool>(type: "INTEGER", nullable: false),
                    PublicoAfirmativo = table.Column<string>(type: "TEXT", maxLength: 120, nullable: true),
                    ObservacoesPcd = table.Column<string>(type: "TEXT", nullable: true),
                    ProjetoNome = table.Column<string>(type: "TEXT", maxLength: 160, nullable: true),
                    ProjetoClienteAreaImpactada = table.Column<string>(type: "TEXT", maxLength: 160, nullable: true),
                    ProjetoPrazoPrevisto = table.Column<string>(type: "TEXT", maxLength: 80, nullable: true),
                    ProjetoDescricao = table.Column<string>(type: "TEXT", nullable: true),
                    Regime = table.Column<short>(type: "INTEGER", nullable: true),
                    CargaSemanalHoras = table.Column<int>(type: "INTEGER", nullable: true),
                    Escala = table.Column<short>(type: "INTEGER", nullable: true),
                    HoraEntrada = table.Column<TimeOnly>(type: "TEXT", nullable: true),
                    HoraSaida = table.Column<TimeOnly>(type: "TEXT", nullable: true),
                    Intervalo = table.Column<TimeSpan>(type: "TEXT", nullable: true),
                    Cep = table.Column<string>(type: "TEXT", maxLength: 12, nullable: true),
                    Logradouro = table.Column<string>(type: "TEXT", maxLength: 160, nullable: true),
                    Numero = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    Bairro = table.Column<string>(type: "TEXT", maxLength: 120, nullable: true),
                    Cidade = table.Column<string>(type: "TEXT", maxLength: 120, nullable: true),
                    Uf = table.Column<string>(type: "TEXT", maxLength: 2, nullable: true),
                    PoliticaTrabalho = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    ObservacoesDeslocamento = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    Moeda = table.Column<short>(type: "INTEGER", nullable: true),
                    SalarioMinimo = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: true),
                    SalarioMaximo = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: true),
                    Periodicidade = table.Column<short>(type: "INTEGER", nullable: true),
                    BonusTipo = table.Column<short>(type: "INTEGER", nullable: true),
                    BonusPercentual = table.Column<decimal>(type: "TEXT", nullable: true),
                    ObservacoesRemuneracao = table.Column<string>(type: "TEXT", maxLength: 240, nullable: true),
                    Escolaridade = table.Column<short>(type: "INTEGER", nullable: true),
                    FormacaoArea = table.Column<short>(type: "INTEGER", nullable: true),
                    ExperienciaMinimaAnos = table.Column<int>(type: "INTEGER", nullable: true),
                    TagsStackRaw = table.Column<string>(type: "TEXT", nullable: true),
                    TagsIdiomasRaw = table.Column<string>(type: "TEXT", nullable: true),
                    Diferenciais = table.Column<string>(type: "TEXT", nullable: true),
                    ObservacoesProcesso = table.Column<string>(type: "TEXT", nullable: true),
                    Visibilidade = table.Column<short>(type: "INTEGER", nullable: true),
                    DataInicio = table.Column<DateOnly>(type: "TEXT", nullable: true),
                    DataEncerramento = table.Column<DateOnly>(type: "TEXT", nullable: true),
                    CanalLinkedIn = table.Column<bool>(type: "INTEGER", nullable: false),
                    CanalSiteCarreiras = table.Column<bool>(type: "INTEGER", nullable: false),
                    CanalIndicacao = table.Column<bool>(type: "INTEGER", nullable: false),
                    CanalPortaisEmprego = table.Column<bool>(type: "INTEGER", nullable: false),
                    DescricaoPublica = table.Column<string>(type: "TEXT", nullable: true),
                    LgpdSolicitarConsentimentoExplicito = table.Column<bool>(type: "INTEGER", nullable: false),
                    LgpdCompartilharCurriculoInternamente = table.Column<bool>(type: "INTEGER", nullable: false),
                    LgpdRetencaoAtiva = table.Column<bool>(type: "INTEGER", nullable: false),
                    LgpdRetencaoMeses = table.Column<int>(type: "INTEGER", nullable: true),
                    ExigeCnh = table.Column<bool>(type: "INTEGER", nullable: false),
                    DisponibilidadeParaViagens = table.Column<bool>(type: "INTEGER", nullable: false),
                    ChecagemAntecedentes = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedAtUtc = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    UpdatedAtUtc = table.Column<DateTimeOffset>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vagas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VagaBeneficios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    TenantId = table.Column<string>(type: "TEXT", maxLength: 64, nullable: false),
                    VagaId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Ordem = table.Column<int>(type: "INTEGER", nullable: false),
                    Tipo = table.Column<short>(type: "INTEGER", nullable: false),
                    Valor = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: true),
                    Recorrencia = table.Column<short>(type: "INTEGER", nullable: false),
                    Obrigatorio = table.Column<bool>(type: "INTEGER", nullable: false),
                    Observacoes = table.Column<string>(type: "TEXT", maxLength: 240, nullable: true),
                    CreatedAtUtc = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    UpdatedAtUtc = table.Column<DateTimeOffset>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VagaBeneficios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VagaBeneficios_Vagas_VagaId",
                        column: x => x.VagaId,
                        principalTable: "Vagas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VagaEtapas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    TenantId = table.Column<string>(type: "TEXT", maxLength: 64, nullable: false),
                    VagaId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Ordem = table.Column<int>(type: "INTEGER", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 160, nullable: false),
                    Responsavel = table.Column<short>(type: "INTEGER", nullable: false),
                    Modo = table.Column<short>(type: "INTEGER", nullable: false),
                    SlaDias = table.Column<int>(type: "INTEGER", nullable: true),
                    DescricaoInstrucoes = table.Column<string>(type: "TEXT", maxLength: 240, nullable: true),
                    CreatedAtUtc = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    UpdatedAtUtc = table.Column<DateTimeOffset>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VagaEtapas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VagaEtapas_Vagas_VagaId",
                        column: x => x.VagaId,
                        principalTable: "Vagas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VagaPerguntas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    TenantId = table.Column<string>(type: "TEXT", maxLength: 64, nullable: false),
                    VagaId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Ordem = table.Column<int>(type: "INTEGER", nullable: false),
                    Texto = table.Column<string>(type: "TEXT", maxLength: 220, nullable: false),
                    Tipo = table.Column<short>(type: "INTEGER", nullable: false),
                    Peso = table.Column<short>(type: "INTEGER", nullable: false),
                    Obrigatoria = table.Column<bool>(type: "INTEGER", nullable: false),
                    Knockout = table.Column<bool>(type: "INTEGER", nullable: false),
                    OpcoesRaw = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    CreatedAtUtc = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    UpdatedAtUtc = table.Column<DateTimeOffset>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VagaPerguntas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VagaPerguntas_Vagas_VagaId",
                        column: x => x.VagaId,
                        principalTable: "Vagas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VagaRequisitos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    TenantId = table.Column<string>(type: "TEXT", maxLength: 64, nullable: false),
                    VagaId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Ordem = table.Column<int>(type: "INTEGER", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 180, nullable: false),
                    Peso = table.Column<short>(type: "INTEGER", nullable: false),
                    Obrigatorio = table.Column<bool>(type: "INTEGER", nullable: false),
                    AnosMinimos = table.Column<int>(type: "INTEGER", nullable: true),
                    Nivel = table.Column<short>(type: "INTEGER", nullable: true),
                    Avaliacao = table.Column<short>(type: "INTEGER", nullable: true),
                    Observacoes = table.Column<string>(type: "TEXT", maxLength: 240, nullable: true),
                    CreatedAtUtc = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    UpdatedAtUtc = table.Column<DateTimeOffset>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VagaRequisitos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VagaRequisitos_Vagas_VagaId",
                        column: x => x.VagaId,
                        principalTable: "Vagas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VagaBeneficios_TenantId_VagaId",
                table: "VagaBeneficios",
                columns: new[] { "TenantId", "VagaId" });

            migrationBuilder.CreateIndex(
                name: "IX_VagaBeneficios_VagaId",
                table: "VagaBeneficios",
                column: "VagaId");

            migrationBuilder.CreateIndex(
                name: "IX_VagaEtapas_TenantId_VagaId",
                table: "VagaEtapas",
                columns: new[] { "TenantId", "VagaId" });

            migrationBuilder.CreateIndex(
                name: "IX_VagaEtapas_VagaId",
                table: "VagaEtapas",
                column: "VagaId");

            migrationBuilder.CreateIndex(
                name: "IX_VagaPerguntas_TenantId_VagaId",
                table: "VagaPerguntas",
                columns: new[] { "TenantId", "VagaId" });

            migrationBuilder.CreateIndex(
                name: "IX_VagaPerguntas_VagaId",
                table: "VagaPerguntas",
                column: "VagaId");

            migrationBuilder.CreateIndex(
                name: "IX_VagaRequisitos_TenantId_VagaId",
                table: "VagaRequisitos",
                columns: new[] { "TenantId", "VagaId" });

            migrationBuilder.CreateIndex(
                name: "IX_VagaRequisitos_VagaId",
                table: "VagaRequisitos",
                column: "VagaId");

            migrationBuilder.CreateIndex(
                name: "IX_Vagas_TenantId_Area",
                table: "Vagas",
                columns: new[] { "TenantId", "Area" });

            migrationBuilder.CreateIndex(
                name: "IX_Vagas_TenantId_Departamento",
                table: "Vagas",
                columns: new[] { "TenantId", "Departamento" });

            migrationBuilder.CreateIndex(
                name: "IX_Vagas_TenantId_Status",
                table: "Vagas",
                columns: new[] { "TenantId", "Status" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VagaBeneficios");

            migrationBuilder.DropTable(
                name: "VagaEtapas");

            migrationBuilder.DropTable(
                name: "VagaPerguntas");

            migrationBuilder.DropTable(
                name: "VagaRequisitos");

            migrationBuilder.DropTable(
                name: "Vagas");
        }
    }
}
