using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RHPortal.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddTenantFiltersForVagaChildren : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_VagaRequisitos_TenantId_VagaId",
                table: "VagaRequisitos",
                columns: new[] { "TenantId", "VagaId" });

            migrationBuilder.CreateIndex(
                name: "IX_VagaPerguntas_TenantId_VagaId",
                table: "VagaPerguntas",
                columns: new[] { "TenantId", "VagaId" });

            migrationBuilder.CreateIndex(
                name: "IX_VagaEtapas_TenantId_VagaId",
                table: "VagaEtapas",
                columns: new[] { "TenantId", "VagaId" });

            migrationBuilder.CreateIndex(
                name: "IX_VagaBeneficios_TenantId_VagaId",
                table: "VagaBeneficios",
                columns: new[] { "TenantId", "VagaId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_VagaRequisitos_TenantId_VagaId",
                table: "VagaRequisitos");

            migrationBuilder.DropIndex(
                name: "IX_VagaPerguntas_TenantId_VagaId",
                table: "VagaPerguntas");

            migrationBuilder.DropIndex(
                name: "IX_VagaEtapas_TenantId_VagaId",
                table: "VagaEtapas");

            migrationBuilder.DropIndex(
                name: "IX_VagaBeneficios_TenantId_VagaId",
                table: "VagaBeneficios");
        }
    }
}
