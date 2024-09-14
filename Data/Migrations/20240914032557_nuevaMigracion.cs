using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MUSEODESCALZOS.Data.Migrations
{
    /// <inheritdoc />
    public partial class nuevaMigracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nombrelmagen",
                table: "tb_Usuario");

            migrationBuilder.DropColumn(
                name: "Nombrelmagen",
                table: "tb_Noticia");

            migrationBuilder.DropColumn(
                name: "Nombrelmagen",
                table: "tb_ImagenAlquiler");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Nombrelmagen",
                table: "tb_Usuario",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nombrelmagen",
                table: "tb_Noticia",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nombrelmagen",
                table: "tb_ImagenAlquiler",
                type: "text",
                nullable: true);
        }
    }
}
