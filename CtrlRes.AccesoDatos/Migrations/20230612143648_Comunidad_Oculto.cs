using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CtrlRes.Data.Migrations
{
    /// <inheritdoc />
    public partial class Comunidad_Oculto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Oculto",
                table: "Comunidad",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Oculto",
                table: "Comunidad");
        }
    }
}
