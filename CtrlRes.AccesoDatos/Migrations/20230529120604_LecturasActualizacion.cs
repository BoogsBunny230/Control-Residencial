using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CtrlRes.Data.Migrations
{
    /// <inheritdoc />
    public partial class LecturasActualizacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Mediciones");

            migrationBuilder.CreateTable(
                name: "RegistroLecturas",
                columns: table => new
                {
                    Folio = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TipLec = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaProgramada = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaRegistro = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Privada_Id = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistroLecturas", x => x.Folio);
                    table.ForeignKey(
                        name: "FK_RegistroLecturas_Privadas_Privada_Id",
                        column: x => x.Privada_Id,
                        principalTable: "Privadas",
                        principalColumn: "IdPriv",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RegistroLecturas_Privada_Id",
                table: "RegistroLecturas",
                column: "Privada_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RegistroLecturas");

            migrationBuilder.CreateTable(
                name: "Mediciones",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Folio = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Fotografia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Medicion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mediciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mediciones_Lecturas_Folio",
                        column: x => x.Folio,
                        principalTable: "Lecturas",
                        principalColumn: "Folio",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Mediciones_Folio",
                table: "Mediciones",
                column: "Folio");
        }
    }
}
