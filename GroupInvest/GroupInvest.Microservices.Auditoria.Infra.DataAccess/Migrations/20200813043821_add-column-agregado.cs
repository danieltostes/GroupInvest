using Microsoft.EntityFrameworkCore.Migrations;

namespace GroupInvest.Microservices.Auditoria.Infra.DataAccess.Migrations
{
    public partial class addcolumnagregado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Agregado",
                table: "Auditorias",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Agregado",
                table: "Auditorias");
        }
    }
}
