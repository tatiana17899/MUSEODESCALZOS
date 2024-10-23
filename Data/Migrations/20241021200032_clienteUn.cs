using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MUSEODESCALZOS.Data.Migrations
{
    /// <inheritdoc />
    public partial class clienteUn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
            name: "IX_tb_PedidoEvento_IDEvento",
            table: "tb_PedidoEvento");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
