using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CtrlRes.Data.Migrations
{
    /// <inheritdoc />
    public partial class ArchivosPagos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Archivo",
                table: "Pagos");

            migrationBuilder.CreateTable(
                name: "ArchivosPagos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Archivo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaSubida = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pagos_Id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArchivosPagos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArchivosPagos_Pagos_Pagos_Id",
                        column: x => x.Pagos_Id,
                        principalTable: "Pagos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArchivosPagos_Pagos_Id",
                table: "ArchivosPagos",
                column: "Pagos_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArchivosPagos");

            migrationBuilder.AddColumn<string>(
                name: "Archivo",
                table: "Pagos",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
