using Microsoft.EntityFrameworkCore.Migrations;

namespace GroupInvest.Microservices.Mensalidades.Infra.DataAccess.Migrations
{
    public partial class addcolumnperiodomensalidade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PeriodoId",
                table: "Mensalidades",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Mensalidades_PeriodoId",
                table: "Mensalidades",
                column: "PeriodoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Mensalidades_Periodos_PeriodoId",
                table: "Mensalidades",
                column: "PeriodoId",
                principalTable: "Periodos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mensalidades_Periodos_PeriodoId",
                table: "Mensalidades");

            migrationBuilder.DropIndex(
                name: "IX_Mensalidades_PeriodoId",
                table: "Mensalidades");

            migrationBuilder.DropColumn(
                name: "PeriodoId",
                table: "Mensalidades");
        }
    }
}
