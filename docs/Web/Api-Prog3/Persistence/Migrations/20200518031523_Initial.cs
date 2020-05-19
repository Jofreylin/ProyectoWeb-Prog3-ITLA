using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CATEGORIA",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(unicode: false, maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CATEGORIA", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PAIS",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(unicode: false, maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PAIS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TIPO_TRABAJO",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(unicode: false, maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TIPO_TRABAJO", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "USER_ADMIN",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Usuario = table.Column<string>(unicode: false, maxLength: 90, nullable: false),
                    Contraseña = table.Column<string>(unicode: false, maxLength: 90, nullable: false),
                    Fecha_Creacion = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    Nombre = table.Column<string>(unicode: false, maxLength: 120, nullable: false),
                    Email = table.Column<string>(unicode: false, maxLength: 90, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER_ADMIN", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CIUDAD",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(unicode: false, maxLength: 60, nullable: false),
                    Nombre_Pais = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CIUDAD", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CIUDAD_PAIS",
                        column: x => x.Nombre_Pais,
                        principalTable: "PAIS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "POST",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre_Categoria = table.Column<int>(nullable: false),
                    Nombre_Tipo_Trabajo = table.Column<int>(nullable: false),
                    Poster = table.Column<int>(nullable: false),
                    Logo = table.Column<byte[]>(nullable: true),
                    Direccion_URL = table.Column<string>(unicode: false, nullable: true),
                    Nombre_Posicion = table.Column<string>(unicode: false, maxLength: 90, nullable: false),
                    Nombre_Calle = table.Column<string>(unicode: false, maxLength: 90, nullable: true),
                    Nombre_Ciudad = table.Column<int>(nullable: false),
                    Nombre_Pais = table.Column<int>(nullable: false),
                    Descripcion = table.Column<string>(unicode: false, nullable: false),
                    Fecha_Creacion = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_POST", x => x.ID);
                    table.ForeignKey(
                        name: "FK_POST_CATEGORIA",
                        column: x => x.Nombre_Categoria,
                        principalTable: "CATEGORIA",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_POST_CIUDAD",
                        column: x => x.Nombre_Ciudad,
                        principalTable: "CIUDAD",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_POST_PAIS",
                        column: x => x.Nombre_Pais,
                        principalTable: "PAIS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_POST_TRABAJO",
                        column: x => x.Nombre_Tipo_Trabajo,
                        principalTable: "TIPO_TRABAJO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "USER_POSTER",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre_Empresa = table.Column<string>(unicode: false, maxLength: 120, nullable: false),
                    Email = table.Column<string>(unicode: false, maxLength: 90, nullable: false),
                    Contra = table.Column<string>(unicode: false, maxLength: 90, nullable: false),
                    Fecha_Creacion = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    Nombre_Calle = table.Column<string>(unicode: false, maxLength: 90, nullable: true),
                    Nombre_Ciudad = table.Column<int>(nullable: false),
                    Nombre_Pais = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER_POSTER", x => x.ID);
                    table.ForeignKey(
                        name: "FK_POSTER_CIUDAD",
                        column: x => x.Nombre_Ciudad,
                        principalTable: "CIUDAD",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_POSTER_PAIS",
                        column: x => x.Nombre_Pais,
                        principalTable: "PAIS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "UQ__CATEGORI__75E3EFCF7C426275",
                table: "CATEGORIA",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CIUDAD_Nombre_Pais",
                table: "CIUDAD",
                column: "Nombre_Pais");

            migrationBuilder.CreateIndex(
                name: "UQ__PAIS__75E3EFCFD08B0375",
                table: "PAIS",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_POST_Nombre_Categoria",
                table: "POST",
                column: "Nombre_Categoria");

            migrationBuilder.CreateIndex(
                name: "IX_POST_Nombre_Ciudad",
                table: "POST",
                column: "Nombre_Ciudad");

            migrationBuilder.CreateIndex(
                name: "IX_POST_Nombre_Pais",
                table: "POST",
                column: "Nombre_Pais");

            migrationBuilder.CreateIndex(
                name: "IX_POST_Nombre_Tipo_Trabajo",
                table: "POST",
                column: "Nombre_Tipo_Trabajo");

            migrationBuilder.CreateIndex(
                name: "UQ__TIPO_TRA__75E3EFCFA3F31137",
                table: "TIPO_TRABAJO",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__USER_ADM__E3237CF72DA8FDC4",
                table: "USER_ADMIN",
                column: "Usuario",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__USER_POS__A9D10534E7793F11",
                table: "USER_POSTER",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_USER_POSTER_Nombre_Ciudad",
                table: "USER_POSTER",
                column: "Nombre_Ciudad");

            migrationBuilder.CreateIndex(
                name: "IX_USER_POSTER_Nombre_Pais",
                table: "USER_POSTER",
                column: "Nombre_Pais");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "POST");

            migrationBuilder.DropTable(
                name: "USER_ADMIN");

            migrationBuilder.DropTable(
                name: "USER_POSTER");

            migrationBuilder.DropTable(
                name: "CATEGORIA");

            migrationBuilder.DropTable(
                name: "TIPO_TRABAJO");

            migrationBuilder.DropTable(
                name: "CIUDAD");

            migrationBuilder.DropTable(
                name: "PAIS");
        }
    }
}
