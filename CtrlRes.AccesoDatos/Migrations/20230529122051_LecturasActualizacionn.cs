using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CtrlRes.Data.Migrations
{
    /// <inheritdoc />
    public partial class LecturasActualizacionn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Lecturas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipLec = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValorLectura = table.Column<decimal>(type: "decimal(7,2)", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NivelRetraso = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaRealizacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegistroLecturas_Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Vivienda_Id = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lecturas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lecturas_RegistroLecturas_RegistroLecturas_Id",
                        column: x => x.RegistroLecturas_Id,
                        principalTable: "RegistroLecturas",
                        principalColumn: "Folio",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArchivoLecturas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Archivo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaSubida = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lectura_Id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArchivoLecturas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArchivoLecturas_Lecturas_Lectura_Id",
                        column: x => x.Lectura_Id,
                        principalTable: "Lecturas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArchivoLecturas_Lectura_Id",
                table: "ArchivoLecturas",
                column: "Lectura_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Lecturas_RegistroLecturas_Id",
                table: "Lecturas",
                column: "RegistroLecturas_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArchivoLecturas");

            migrationBuilder.DropTable(
                name: "Lecturas");
        }
    }
}
