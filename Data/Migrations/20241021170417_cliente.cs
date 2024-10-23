using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MUSEODESCALZOS.Data.Migrations
{
    /// <inheritdoc />
    public partial class cliente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "tb_Cliente",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_Cliente_UserId",
                table: "tb_Cliente",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_Cliente_AspNetUsers_UserId",
                table: "tb_Cliente",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
        }
    }
}
