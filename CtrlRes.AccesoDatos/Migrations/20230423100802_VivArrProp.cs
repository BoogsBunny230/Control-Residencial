using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CtrlRes.Data.Migrations
{
    /// <inheritdoc />
    public partial class VivArrProp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Arrendatarios_Privadas_Privada",
                table: "Arrendatarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Propietarios",
                table: "Propietarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Arrendatarios",
                table: "Arrendatarios");

            migrationBuilder.DropIndex(
                name: "IX_Arrendatarios_Privada",
                table: "Arrendatarios");

            migrationBuilder.DropColumn(
                name: "MedAg",
                table: "Propietarios");

            migrationBuilder.DropColumn(
                name: "NomProp",
                table: "Propietarios");

            migrationBuilder.DropColumn(
                name: "Privada",
                table: "Arrendatarios");

            migrationBuilder.RenameColumn(
                name: "Privada",
                table: "Propietarios",
                newName: "Nombre");

            migrationBuilder.RenameColumn(
                name: "NumViv",
                table: "Propietarios",
                newName: "Vivienda_Id");

            migrationBuilder.RenameColumn(
                name: "Propietario",
                table: "Arrendatarios",
                newName: "Vehiculo");

            migrationBuilder.RenameColumn(
                name: "NumViv",
                table: "Arrendatarios",
                newName: "Vivienda_Id");

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "Propietarios",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "Arrendatarios",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Propietarios",
                table: "Propietarios",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Arrendatarios",
                table: "Arrendatarios",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Viviendas",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Numero = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MedidorAgua = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Privada_Id = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Viviendas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Viviendas_Privadas_Privada_Id",
                        column: x => x.Privada_Id,
                        principalTable: "Privadas",
                        principalColumn: "IdPriv",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Propietarios_Vivienda_Id",
                table: "Propietarios",
                column: "Vivienda_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Arrendatarios_Vivienda_Id",
                table: "Arrendatarios",
                column: "Vivienda_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Viviendas_Privada_Id",
                table: "Viviendas",
                column: "Privada_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Arrendatarios_Viviendas_Vivienda_Id",
                table: "Arrendatarios",
                column: "Vivienda_Id",
                principalTable: "Viviendas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Propietarios_Viviendas_Vivienda_Id",
                table: "Propietarios",
                column: "Vivienda_Id",
                principalTable: "Viviendas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Arrendatarios_Viviendas_Vivienda_Id",
                table: "Arrendatarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Propietarios_Viviendas_Vivienda_Id",
                table: "Propietarios");

            migrationBuilder.DropTable(
                name: "Viviendas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Propietarios",
                table: "Propietarios");

            migrationBuilder.DropIndex(
                name: "IX_Propietarios_Vivienda_Id",
                table: "Propietarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Arrendatarios",
                table: "Arrendatarios");

            migrationBuilder.DropIndex(
                name: "IX_Arrendatarios_Vivienda_Id",
                table: "Arrendatarios");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Propietarios");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Arrendatarios");

            migrationBuilder.RenameColumn(
                name: "Vivienda_Id",
                table: "Propietarios",
                newName: "NumViv");

            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "Propietarios",
                newName: "Privada");

            migrationBuilder.RenameColumn(
                name: "Vivienda_Id",
                table: "Arrendatarios",
                newName: "NumViv");

            migrationBuilder.RenameColumn(
                name: "Vehiculo",
                table: "Arrendatarios",
                newName: "Propietario");

            migrationBuilder.AddColumn<string>(
                name: "MedAg",
                table: "Propietarios",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NomProp",
                table: "Propietarios",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Privada",
                table: "Arrendatarios",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Propietarios",
                table: "Propietarios",
                column: "NumViv");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Arrendatarios",
                table: "Arrendatarios",
                column: "NumViv");

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
    }
}
