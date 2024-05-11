using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CtrlRes.Data.Migrations
{
    /// <inheritdoc />
    public partial class SancionesActualizacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Fecha",
                table: "Sanciones",
                newName: "FechaIncidente");

            migrationBuilder.AddColumn<string>(
                name: "FechaRegistro",
                table: "Sanciones",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
            name: "FechaIncidente",
            table: "Sanciones",
            newName: "Fecha");

            migrationBuilder.DropColumn(
                name: "FechaRegistro",
                table: "Sanciones");
        }
    }
}
