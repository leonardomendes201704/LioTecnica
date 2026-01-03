using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RHPortal.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddCollumnVagasDepartmentId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Vagas_TenantId_Departamento",
                table: "Vagas");

            migrationBuilder.DropColumn(
                name: "Departamento",
                table: "Vagas");

            migrationBuilder.AddColumn<Guid>(
                name: "DepartmentId",
                table: "Vagas",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Vagas_DepartmentId",
                table: "Vagas",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Vagas_TenantId_DepartmentId",
                table: "Vagas",
                columns: new[] { "TenantId", "DepartmentId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Vagas_Departments_DepartmentId",
                table: "Vagas",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vagas_Departments_DepartmentId",
                table: "Vagas");

            migrationBuilder.DropIndex(
                name: "IX_Vagas_DepartmentId",
                table: "Vagas");

            migrationBuilder.DropIndex(
                name: "IX_Vagas_TenantId_DepartmentId",
                table: "Vagas");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Vagas");

            migrationBuilder.AddColumn<short>(
                name: "Departamento",
                table: "Vagas",
                type: "INTEGER",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.CreateIndex(
                name: "IX_Vagas_TenantId_Departamento",
                table: "Vagas",
                columns: new[] { "TenantId", "Departamento" });
        }
    }
}
