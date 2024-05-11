using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CtrlRes.Data.Migrations
{
    /// <inheritdoc />
    public partial class PrivadasClaveExterna : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ArchivosPDFId",
                table: "Privadas",
                type: "int",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Privadas_ArchivosPDFId",
                table: "Privadas",
                column: "ArchivosPDFId");

            migrationBuilder.AddForeignKey(
                name: "FK_Privadas_ArchivosPDF_ArchivosPDFId",
                table: "Privadas",
                column: "ArchivosPDFId",
                principalTable: "ArchivosPDF",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Privadas_ArchivosPDF_ArchivosPDFId",
                table: "Privadas");

            migrationBuilder.DropIndex(
                name: "IX_Privadas_ArchivosPDFId",
                table: "Privadas");

            migrationBuilder.DropColumn(
                name: "ArchivosPDFId",
                table: "Privadas");
        }
    }
}
