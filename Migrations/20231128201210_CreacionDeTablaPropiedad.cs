using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PropiedadesMinimalApi.Migrations
{
    /// <inheritdoc />
    public partial class CreacionDeTablaPropiedad : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Propiedad",
                columns: table => new
                {
                    IdPropiedad = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ubicacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Activa = table.Column<bool>(type: "bit", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Propiedad", x => x.IdPropiedad);
                });

            migrationBuilder.InsertData(
                table: "Propiedad",
                columns: new[] { "IdPropiedad", "Activa", "Descripcion", "FechaCreacion", "Nombre", "Ubicacion" },
                values: new object[,]
                {
                    { 1, true, "Es la de estados unidos", new DateTime(2023, 11, 28, 14, 12, 6, 175, DateTimeKind.Local).AddTicks(730), "Casa Blanca", "Washintong D.C" },
                    { 2, false, "Es la de estados Verdes", new DateTime(2023, 11, 28, 14, 12, 6, 175, DateTimeKind.Local).AddTicks(755), "Casa Azul", "Arizona D.C" },
                    { 3, true, "Es la de estados Rojos", new DateTime(2023, 11, 28, 14, 12, 6, 175, DateTimeKind.Local).AddTicks(760), "Casa Negra", "California D.C" },
                    { 4, false, "Es la de estados Amarillos", new DateTime(2023, 11, 28, 14, 12, 6, 175, DateTimeKind.Local).AddTicks(765), "Casa Rosa", "Washintong C.D" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Propiedad");
        }
    }
}
