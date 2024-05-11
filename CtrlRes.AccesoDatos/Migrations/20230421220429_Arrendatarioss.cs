using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CtrlRes.Data.Migrations
{
    /// <inheritdoc />
    public partial class Arrendatarioss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
       
            migrationBuilder.AddColumn<string>(
                name: "Privada",
                table: "Arrendatarios",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Arrendatarios_Privada",
                table: "Arrendatarios",
                column: "Privada");

            migrationBuilder.AddForeignKey(
                name: "FK_Arrendatarios_Privadas_Privada",
                table: "Arrendatarios",
                column: "Privada",
                principalTable: "Privadas",
                principalColumn: "IdPriv");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Arrendatarios_Privadas_Privada",
                table: "Arrendatarios");


            migrationBuilder.DropIndex(
                name: "IX_Arrendatarios_Privada",
                table: "Arrendatarios");

            migrationBuilder.DropColumn(
                name: "Privada",
                table: "Arrendatarios");

        }
    }
}
