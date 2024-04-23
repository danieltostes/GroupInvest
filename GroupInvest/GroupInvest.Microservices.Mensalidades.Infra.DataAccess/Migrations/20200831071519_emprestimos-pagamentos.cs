using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GroupInvest.Microservices.Mensalidades.Infra.DataAccess.Migrations
{
    public partial class emprestimospagamentos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Emprestimos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AdesaoId = table.Column<int>(nullable: true),
                    Data = table.Column<DateTime>(nullable: false),
                    DataProximoVencimento = table.Column<DateTime>(nullable: true),
                    Valor = table.Column<decimal>(nullable: false),
                    Saldo = table.Column<decimal>(nullable: false),
                    Quitado = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emprestimos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Emprestimos_Adesoes_AdesaoId",
                        column: x => x.AdesaoId,
                        principalTable: "Adesoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pagamentos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DataPagamento = table.Column<DateTime>(nullable: false),
                    ValorTotalPagamento = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pagamentos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PagamentosEmprestimos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EmprestimoId = table.Column<int>(nullable: true),
                    DataPagamento = table.Column<DateTime>(nullable: false),
                    ValorPagamento = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PagamentosEmprestimos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PagamentosEmprestimos_Emprestimos_EmprestimoId",
                        column: x => x.EmprestimoId,
                        principalTable: "Emprestimos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ItensPagamento",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PagamentoId = table.Column<int>(nullable: true),
                    MensalidadeId = table.Column<int>(nullable: true),
                    EmprestimoId = table.Column<int>(nullable: true),
                    Valor = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItensPagamento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItensPagamento_Emprestimos_EmprestimoId",
                        column: x => x.EmprestimoId,
                        principalTable: "Emprestimos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ItensPagamento_Mensalidades_MensalidadeId",
                        column: x => x.MensalidadeId,
                        principalTable: "Mensalidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ItensPagamento_Pagamentos_PagamentoId",
                        column: x => x.PagamentoId,
                        principalTable: "Pagamentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Emprestimos_AdesaoId",
                table: "Emprestimos",
                column: "AdesaoId");

            migrationBuilder.CreateIndex(
                name: "IX_ItensPagamento_EmprestimoId",
                table: "ItensPagamento",
                column: "EmprestimoId");

            migrationBuilder.CreateIndex(
                name: "IX_ItensPagamento_MensalidadeId",
                table: "ItensPagamento",
                column: "MensalidadeId");

            migrationBuilder.CreateIndex(
                name: "IX_ItensPagamento_PagamentoId",
                table: "ItensPagamento",
                column: "PagamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_PagamentosEmprestimos_EmprestimoId",
                table: "PagamentosEmprestimos",
                column: "EmprestimoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItensPagamento");

            migrationBuilder.DropTable(
                name: "PagamentosEmprestimos");

            migrationBuilder.DropTable(
                name: "Pagamentos");

            migrationBuilder.DropTable(
                name: "Emprestimos");
        }
    }
}
