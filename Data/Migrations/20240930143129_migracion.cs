using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MUSEODESCALZOS.Data.Migrations
{
    /// <inheritdoc />
    public partial class migracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_Cliente_tb_Usuario_IDUsuario",
                table: "tb_Cliente");

            migrationBuilder.DropTable(
                name: "tb_Usuario");

            migrationBuilder.DropIndex(
                name: "IX_tb_Cliente_IDUsuario",
                table: "tb_Cliente");

            migrationBuilder.DropColumn(
                name: "IDUsuario",
                table: "tb_Cliente");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
        }
    }
}
