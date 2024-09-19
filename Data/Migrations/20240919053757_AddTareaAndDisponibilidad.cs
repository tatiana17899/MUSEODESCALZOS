using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MUSEODESCALZOS.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddTareaAndDisponibilidad : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Disponible",
                table: "tb_Guía",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "tb_Tarea",
                columns: table => new
                {
                    IDTarea = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GuíaID = table.Column<long>(type: "bigint", nullable: false),
                    Descripción = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_Tarea", x => x.IDTarea);
                    table.ForeignKey(
                        name: "FK_tb_Tarea_tb_Guía_GuíaID",
                        column: x => x.GuíaID,
                        principalTable: "tb_Guía",
                        principalColumn: "IDGuía",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_Tarea_GuíaID",
                table: "tb_Tarea",
                column: "GuíaID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_Tarea");

            migrationBuilder.DropColumn(
                name: "Disponible",
                table: "tb_Guía");
        }
    }
}
