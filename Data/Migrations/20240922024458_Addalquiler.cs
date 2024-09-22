using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MUSEODESCALZOS.Data.Migrations
{
    /// <inheritdoc />
    public partial class Addalquiler : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rol",
                table: "tb_Administrador");

            migrationBuilder.AddColumn<string>(
                name: "ContraseñaGenerada",
                table: "tb_Guía",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContraseñaGenerada",
                table: "tb_Guía");

            migrationBuilder.AddColumn<string>(
                name: "Rol",
                table: "tb_Administrador",
                type: "text",
                nullable: true);
        }
    }
}
