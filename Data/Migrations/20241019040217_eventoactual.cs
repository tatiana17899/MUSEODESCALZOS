using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MUSEODESCALZOS.Data.Migrations
{
    /// <inheritdoc />
    public partial class eventoactual : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_PedidoEvento_tb_Evento_IDEvento",
                table: "tb_PedidoEvento"
            );

            // Agregar la nueva clave foránea para Evento
            migrationBuilder.AddForeignKey(
                name: "FK_tb_PedidoEvento_tb_Evento_IDEvento",
                table: "tb_PedidoEvento",
                column: "IDEvento",
                principalTable: "tb_Evento",
                principalColumn: "IDEvento",
                onDelete: ReferentialAction.Cascade
            );

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
