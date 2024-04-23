using Microsoft.EntityFrameworkCore.Migrations;

namespace GroupInvest.Microservices.Mensalidades.Infra.DataAccess.Migrations
{
    public partial class conversoesprevisaopagamentoemprestimo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Realizada",
                table: "PrevisoesPagamentoEmprestimo",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)");

            migrationBuilder.AlterColumn<string>(
                name: "Consolidada",
                table: "PrevisoesPagamentoEmprestimo",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Realizada",
                table: "PrevisoesPagamentoEmprestimo",
                type: "tinyint(1)",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<bool>(
                name: "Consolidada",
                table: "PrevisoesPagamentoEmprestimo",
                type: "tinyint(1)",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
