using Microsoft.EntityFrameworkCore.Migrations;

namespace GroupInvest.Microservices.Mensalidades.Infra.DataAccess.Migrations
{
    public partial class itempagamentoprevisaopagamentoemprestimo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItensPagamento_Emprestimos_EmprestimoId",
                table: "ItensPagamento");

            migrationBuilder.DropIndex(
                name: "IX_ItensPagamento_EmprestimoId",
                table: "ItensPagamento");

            migrationBuilder.DropColumn(
                name: "EmprestimoId",
                table: "ItensPagamento");

            migrationBuilder.AddColumn<int>(
                name: "PrevisaoPagamentoEmprestimoId",
                table: "ItensPagamento",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItensPagamento_PrevisaoPagamentoEmprestimoId",
                table: "ItensPagamento",
                column: "PrevisaoPagamentoEmprestimoId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItensPagamento_PrevisoesPagamentoEmprestimo_PrevisaoPagament~",
                table: "ItensPagamento",
                column: "PrevisaoPagamentoEmprestimoId",
                principalTable: "PrevisoesPagamentoEmprestimo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItensPagamento_PrevisoesPagamentoEmprestimo_PrevisaoPagament~",
                table: "ItensPagamento");

            migrationBuilder.DropIndex(
                name: "IX_ItensPagamento_PrevisaoPagamentoEmprestimoId",
                table: "ItensPagamento");

            migrationBuilder.DropColumn(
                name: "PrevisaoPagamentoEmprestimoId",
                table: "ItensPagamento");

            migrationBuilder.AddColumn<int>(
                name: "EmprestimoId",
                table: "ItensPagamento",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItensPagamento_EmprestimoId",
                table: "ItensPagamento",
                column: "EmprestimoId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItensPagamento_Emprestimos_EmprestimoId",
                table: "ItensPagamento",
                column: "EmprestimoId",
                principalTable: "Emprestimos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
