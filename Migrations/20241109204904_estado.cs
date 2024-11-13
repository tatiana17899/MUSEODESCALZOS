using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MUSEODESCALZOS.Migrations
{
    /// <inheritdoc />
    public partial class estado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Estado",
                table: "tb_PedidoVisita",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
           
        }
    }
}
