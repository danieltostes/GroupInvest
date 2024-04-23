using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GroupInvest.Microservices.Mensalidades.Infra.DataAccess.Migrations
{
    public partial class previsaopagamentoemprestimo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PrevisoesPagamentoEmprestimo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EmprestimoId = table.Column<int>(nullable: true),
                    DataVencimento = table.Column<DateTime>(nullable: false),
                    ValorBase = table.Column<decimal>(nullable: false),
                    ValorDevido = table.Column<decimal>(nullable: false),
                    PercentualJuros = table.Column<decimal>(nullable: false),
                    Consolidada = table.Column<bool>(nullable: false),
                    Realizada = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrevisoesPagamentoEmprestimo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrevisoesPagamentoEmprestimo_Emprestimos_EmprestimoId",
                        column: x => x.EmprestimoId,
                        principalTable: "Emprestimos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PrevisoesPagamentoEmprestimo_EmprestimoId",
                table: "PrevisoesPagamentoEmprestimo",
                column: "EmprestimoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PrevisoesPagamentoEmprestimo");
        }
    }
}
