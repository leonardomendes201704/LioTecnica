using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RHPortal.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddCollumnVagasAreaId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Vagas_TenantId_Area",
                table: "Vagas");

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

            migrationBuilder.DropColumn(
                name: "Area",
                table: "Vagas");

            migrationBuilder.AddColumn<Guid>(
                name: "AreaId",
                table: "Vagas",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Vagas_AreaId",
                table: "Vagas",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Vagas_TenantId_AreaId",
                table: "Vagas",
                columns: new[] { "TenantId", "AreaId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Vagas_Areas_AreaId",
                table: "Vagas",
                column: "AreaId",
                principalTable: "Areas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vagas_Areas_AreaId",
                table: "Vagas");

            migrationBuilder.DropIndex(
                name: "IX_Vagas_AreaId",
                table: "Vagas");

            migrationBuilder.DropIndex(
                name: "IX_Vagas_TenantId_AreaId",
                table: "Vagas");

            migrationBuilder.DropColumn(
                name: "AreaId",
                table: "Vagas");

            migrationBuilder.AddColumn<short>(
                name: "Area",
                table: "Vagas",
                type: "INTEGER",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.CreateIndex(
                name: "IX_Vagas_TenantId_Area",
                table: "Vagas",
                columns: new[] { "TenantId", "Area" });

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
    }
}
