using Microsoft.EntityFrameworkCore.Migrations;

namespace GroupInvest.Microservices.Participantes.Infra.DataAccess.Migrations
{
    public partial class updateparticipanteusuarioappidnullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UsuarioAplicativoId",
                table: "Participantes",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UsuarioAplicativoId",
                table: "Participantes",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
