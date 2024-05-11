using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CtrlRes.Data.Migrations
{
    /// <inheritdoc />
    public partial class SancionesMantenimientosAsambleas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Sanciones",
                table: "Sanciones");

            migrationBuilder.RenameColumn(
                name: "Sancion",
                table: "Sanciones",
                newName: "Notificacion");

            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "Sanciones",
                newName: "Mensaje");

            migrationBuilder.RenameColumn(
                name: "IdSan",
                table: "Sanciones",
                newName: "Vivienda_Id");

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "Sanciones",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Concepto",
                table: "Sanciones",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Fecha",
                table: "Sanciones",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<float>(
                name: "Monto",
                table: "Sanciones",
                type: "real",
                nullable: false,
                defaultValue: 0f);


            migrationBuilder.AddPrimaryKey(
                name: "PK_Sanciones",
                table: "Sanciones",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Sanciones_Vivienda_Id",
                table: "Sanciones",
                column: "Vivienda_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sanciones_Viviendas_Vivienda_Id",
                table: "Sanciones",
                column: "Vivienda_Id",
                principalTable: "Viviendas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropForeignKey(
                name: "FK_Sanciones_Viviendas_Vivienda_Id",
                table: "Sanciones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sanciones",
                table: "Sanciones");

            migrationBuilder.DropIndex(
                name: "IX_Sanciones_Vivienda_Id",
                table: "Sanciones");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Sanciones");

            migrationBuilder.DropColumn(
                name: "Concepto",
                table: "Sanciones");

            migrationBuilder.DropColumn(
                name: "Fecha",
                table: "Sanciones");

            migrationBuilder.DropColumn(
                name: "Monto",
                table: "Sanciones");


            migrationBuilder.RenameColumn(
                name: "Vivienda_Id",
                table: "Sanciones",
                newName: "IdSan");

            migrationBuilder.RenameColumn(
                name: "Notificacion",
                table: "Sanciones",
                newName: "Sancion");

            migrationBuilder.RenameColumn(
                name: "Mensaje",
                table: "Sanciones",
                newName: "Nombre");


            migrationBuilder.AddPrimaryKey(
                name: "PK_Sanciones",
                table: "Sanciones",
                column: "IdSan");

        }
    }
}
