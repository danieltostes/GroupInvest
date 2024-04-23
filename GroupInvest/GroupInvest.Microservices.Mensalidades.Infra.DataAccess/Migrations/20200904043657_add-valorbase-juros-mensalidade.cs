using Microsoft.EntityFrameworkCore.Migrations;

namespace GroupInvest.Microservices.Mensalidades.Infra.DataAccess.Migrations
{
    public partial class addvalorbasejurosmensalidade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PercentualJuros",
                table: "Mensalidades",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ValorBase",
                table: "Mensalidades",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PercentualJuros",
                table: "Mensalidades");

            migrationBuilder.DropColumn(
                name: "ValorBase",
                table: "Mensalidades");
        }
    }
}
