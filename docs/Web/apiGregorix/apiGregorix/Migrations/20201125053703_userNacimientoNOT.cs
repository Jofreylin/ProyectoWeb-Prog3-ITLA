using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace apiGregorix.Migrations
{
    public partial class userNacimientoNOT : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fecha_nacimiento",
                table: "USER_POSTER");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Fecha_nacimiento",
                table: "USER_POSTER",
                type: "datetime",
                nullable: true,
                defaultValueSql: "('0001-01-01T00:00:00.000')");
        }
    }
}
