using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MUSEODESCALZOS.Data.Migrations
{
    /// <inheritdoc />
    public partial class MigracionAlquiler : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_tb_PedidoAlquiler_IDAlquiler",
                table: "tb_PedidoAlquiler");

            migrationBuilder.RenameColumn(
                name: "IDAlquileres",
                table: "tb_Alquiler",
                newName: "IdAlquiler");

            migrationBuilder.AlterColumn<bool>(
                name: "Disponible",
                table: "tb_Alquiler",
                type: "boolean",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<int>(
                name: "Capacidad",
                table: "tb_Alquiler",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateIndex(
                name: "IX_tb_PedidoAlquiler_IDAlquiler",
                table: "tb_PedidoAlquiler",
                column: "IDAlquiler");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_tb_PedidoAlquiler_IDAlquiler",
                table: "tb_PedidoAlquiler");

            migrationBuilder.RenameColumn(
                name: "IdAlquiler",
                table: "tb_Alquiler",
                newName: "IDAlquileres");

            migrationBuilder.AlterColumn<bool>(
                name: "Disponible",
                table: "tb_Alquiler",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Capacidad",
                table: "tb_Alquiler",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_PedidoAlquiler_IDAlquiler",
                table: "tb_PedidoAlquiler",
                column: "IDAlquiler",
                unique: true);
        }
    }
}
