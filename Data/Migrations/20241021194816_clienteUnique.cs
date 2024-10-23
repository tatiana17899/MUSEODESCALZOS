using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MUSEODESCALZOS.Data.Migrations
{
    /// <inheritdoc />
    public partial class clienteUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
            name: "IX_tb_PedidoEvento_IDEvento",
            table: "tb_PedidoEvento");

            migrationBuilder.CreateIndex(
            name: "uq_cliente_evento",
            table: "tb_PedidoEvento",
            columns: new[] { "IDCliente", "IDEvento" },
            unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
