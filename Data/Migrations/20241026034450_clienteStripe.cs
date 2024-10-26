using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MUSEODESCALZOS.Data.Migrations
{
    /// <inheritdoc />
    public partial class clienteStripe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StripeCustomerId",
                table: "tb_Cliente",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
           
        }
    }
}
