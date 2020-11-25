using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace apiGregorix.Migrations
{
    public partial class userNacimientoToNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Fecha_nacimiento",
                table: "USER_POSTER",
                type: "datetime",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Fecha_nacimiento",
                table: "USER_POSTER",
                type: "datetime",
                nullable: false,
                defaultValueSql: "('0001-01-01T00:00:00.000')",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true,
                oldDefaultValueSql: "('0001-01-01T00:00:00.000')");
        }
    }
}
