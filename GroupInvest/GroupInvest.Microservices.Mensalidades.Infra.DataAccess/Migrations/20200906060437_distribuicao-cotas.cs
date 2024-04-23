using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GroupInvest.Microservices.Mensalidades.Infra.DataAccess.Migrations
{
    public partial class distribuicaocotas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DistribuicaoCotas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PeriodoId = table.Column<int>(nullable: true),
                    ValorPrevisto = table.Column<decimal>(nullable: false),
                    ValorArrecadado = table.Column<decimal>(nullable: false),
                    NumeroTotalCotas = table.Column<int>(nullable: false),
                    PercentualRendimento = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistribuicaoCotas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DistribuicaoCotas_Periodos_PeriodoId",
                        column: x => x.PeriodoId,
                        principalTable: "Periodos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DistribuicaoParticipante",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DistribuicaoCotasId = table.Column<int>(nullable: true),
                    ParticipanteId = table.Column<int>(nullable: true),
                    ValorDistribuicao = table.Column<decimal>(nullable: false),
                    SaldoDevedor = table.Column<decimal>(nullable: false),
                    ValorTotalReceber = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistribuicaoParticipante", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DistribuicaoParticipante_DistribuicaoCotas_DistribuicaoCotas~",
                        column: x => x.DistribuicaoCotasId,
                        principalTable: "DistribuicaoCotas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DistribuicaoParticipante_Participantes_ParticipanteId",
                        column: x => x.ParticipanteId,
                        principalTable: "Participantes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DistribuicaoCotas_PeriodoId",
                table: "DistribuicaoCotas",
                column: "PeriodoId");

            migrationBuilder.CreateIndex(
                name: "IX_DistribuicaoParticipante_DistribuicaoCotasId",
                table: "DistribuicaoParticipante",
                column: "DistribuicaoCotasId");

            migrationBuilder.CreateIndex(
                name: "IX_DistribuicaoParticipante_ParticipanteId",
                table: "DistribuicaoParticipante",
                column: "ParticipanteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DistribuicaoParticipante");

            migrationBuilder.DropTable(
                name: "DistribuicaoCotas");
        }
    }
}
