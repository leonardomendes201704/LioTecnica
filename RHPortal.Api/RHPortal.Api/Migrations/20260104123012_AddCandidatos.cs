using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RHPortal.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddCandidatos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Candidatos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    TenantId = table.Column<string>(type: "TEXT", maxLength: 64, nullable: false),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 160, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 180, nullable: false),
                    Fone = table.Column<string>(type: "TEXT", maxLength: 40, nullable: true),
                    Cidade = table.Column<string>(type: "TEXT", maxLength: 120, nullable: true),
                    Uf = table.Column<string>(type: "TEXT", maxLength: 2, nullable: true),
                    Fonte = table.Column<int>(type: "INTEGER", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    VagaId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Obs = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: true),
                    CvText = table.Column<string>(type: "TEXT", nullable: true),
                    LastMatchScore = table.Column<int>(type: "INTEGER", nullable: true),
                    LastMatchPass = table.Column<bool>(type: "INTEGER", nullable: true),
                    LastMatchAtUtc = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    LastMatchVagaId = table.Column<Guid>(type: "TEXT", nullable: true),
                    CreatedAtUtc = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    UpdatedAtUtc = table.Column<DateTimeOffset>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidatos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Candidatos_Vagas_VagaId",
                        column: x => x.VagaId,
                        principalTable: "Vagas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CandidatoDocumentos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    TenantId = table.Column<string>(type: "TEXT", maxLength: 64, nullable: false),
                    CandidatoId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Tipo = table.Column<int>(type: "INTEGER", nullable: false),
                    NomeArquivo = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    ContentType = table.Column<string>(type: "TEXT", maxLength: 120, nullable: true),
                    Descricao = table.Column<string>(type: "TEXT", maxLength: 240, nullable: true),
                    TamanhoBytes = table.Column<long>(type: "INTEGER", nullable: true),
                    Url = table.Column<string>(type: "TEXT", maxLength: 400, nullable: true),
                    CreatedAtUtc = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    UpdatedAtUtc = table.Column<DateTimeOffset>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidatoDocumentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CandidatoDocumentos_Candidatos_CandidatoId",
                        column: x => x.CandidatoId,
                        principalTable: "Candidatos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CandidatoDocumentos_CandidatoId",
                table: "CandidatoDocumentos",
                column: "CandidatoId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidatoDocumentos_TenantId_CandidatoId",
                table: "CandidatoDocumentos",
                columns: new[] { "TenantId", "CandidatoId" });

            migrationBuilder.CreateIndex(
                name: "IX_Candidatos_TenantId_Email",
                table: "Candidatos",
                columns: new[] { "TenantId", "Email" });

            migrationBuilder.CreateIndex(
                name: "IX_Candidatos_TenantId_VagaId",
                table: "Candidatos",
                columns: new[] { "TenantId", "VagaId" });

            migrationBuilder.CreateIndex(
                name: "IX_Candidatos_VagaId",
                table: "Candidatos",
                column: "VagaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CandidatoDocumentos");

            migrationBuilder.DropTable(
                name: "Candidatos");
        }
    }
}
