using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GroupInvest.Microservices.Participantes.Infra.DataAccess.Migrations
{
    public partial class addcolumnsadesaocotas_datacadastramento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataAdesao",
                table: "Adesoes",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "NumeroCotas",
                table: "Adesoes",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataAdesao",
                table: "Adesoes");

            migrationBuilder.DropColumn(
                name: "NumeroCotas",
                table: "Adesoes");
        }
    }
}
