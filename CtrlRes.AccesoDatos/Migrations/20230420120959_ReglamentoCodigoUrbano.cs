using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CtrlRes.Data.Migrations
{
    /// <inheritdoc />
    public partial class ReglamentoCodigoUrbano : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<int>(
                name: "ArchivosPDFIdCU",
                table: "Privadas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Privadas_ArchivosPDFIdCU",
                table: "Privadas",
                column: "ArchivosPDFIdCU");

            migrationBuilder.AddForeignKey(
                name: "FK_Privadas_ArchivosPDF_ArchivosPDFIdCU",
                table: "Privadas",
                column: "ArchivosPDFIdCU",
                principalTable: "ArchivosPDF",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Privadas_ArchivosPDF_ArchivosPDFIdCU",
                table: "Privadas");

            migrationBuilder.DropIndex(
                name: "IX_Privadas_ArchivosPDFIdCU",
                table: "Privadas");

            migrationBuilder.DropColumn(
                name: "ArchivosPDFIdCU",
                table: "Privadas");

        }
    }
}
