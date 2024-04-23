using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GroupInvest.Microservices.Mensalidades.Infra.DataAccess.Migrations
{
    public partial class addadesao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mensalidades_Participantes_ParticipanteId",
                table: "Mensalidades");

            migrationBuilder.DropForeignKey(
                name: "FK_Mensalidades_Periodos_PeriodoId",
                table: "Mensalidades");

            migrationBuilder.DropIndex(
                name: "IX_Mensalidades_ParticipanteId",
                table: "Mensalidades");

            migrationBuilder.DropIndex(
                name: "IX_Mensalidades_PeriodoId",
                table: "Mensalidades");

            migrationBuilder.DropColumn(
                name: "ParticipanteId",
                table: "Mensalidades");

            migrationBuilder.DropColumn(
                name: "PeriodoId",
                table: "Mensalidades");

            migrationBuilder.AddColumn<string>(
                name: "Ativo",
                table: "Periodos",
                nullable: false);

            migrationBuilder.AddColumn<int>(
                name: "AdesaoId",
                table: "Mensalidades",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Adesoes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ParticipanteId = table.Column<int>(nullable: true),
                    PeriodoId = table.Column<int>(nullable: true),
                    NumeroCotas = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adesoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Adesoes_Participantes_ParticipanteId",
                        column: x => x.ParticipanteId,
                        principalTable: "Participantes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Adesoes_Periodos_PeriodoId",
                        column: x => x.PeriodoId,
                        principalTable: "Periodos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Mensalidades_AdesaoId",
                table: "Mensalidades",
                column: "AdesaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Adesoes_ParticipanteId",
                table: "Adesoes",
                column: "ParticipanteId");

            migrationBuilder.CreateIndex(
                name: "IX_Adesoes_PeriodoId",
                table: "Adesoes",
                column: "PeriodoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Mensalidades_Adesoes_AdesaoId",
                table: "Mensalidades",
                column: "AdesaoId",
                principalTable: "Adesoes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mensalidades_Adesoes_AdesaoId",
                table: "Mensalidades");

            migrationBuilder.DropTable(
                name: "Adesoes");

            migrationBuilder.DropIndex(
                name: "IX_Mensalidades_AdesaoId",
                table: "Mensalidades");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Periodos");

            migrationBuilder.DropColumn(
                name: "AdesaoId",
                table: "Mensalidades");

            migrationBuilder.AddColumn<int>(
                name: "ParticipanteId",
                table: "Mensalidades",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PeriodoId",
                table: "Mensalidades",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Mensalidades_ParticipanteId",
                table: "Mensalidades",
                column: "ParticipanteId");

            migrationBuilder.CreateIndex(
                name: "IX_Mensalidades_PeriodoId",
                table: "Mensalidades",
                column: "PeriodoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Mensalidades_Participantes_ParticipanteId",
                table: "Mensalidades",
                column: "ParticipanteId",
                principalTable: "Participantes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Mensalidades_Periodos_PeriodoId",
                table: "Mensalidades",
                column: "PeriodoId",
                principalTable: "Periodos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
