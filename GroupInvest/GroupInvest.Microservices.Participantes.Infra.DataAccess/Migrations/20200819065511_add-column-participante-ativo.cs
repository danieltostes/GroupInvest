using Microsoft.EntityFrameworkCore.Migrations;

namespace GroupInvest.Microservices.Participantes.Infra.DataAccess.Migrations
{
    public partial class addcolumnparticipanteativo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Ativo",
                table: "Participantes",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Participantes");
        }
    }
}
