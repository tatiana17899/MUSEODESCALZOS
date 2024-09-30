using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MUSEODESCALZOS.Data.Migrations
{
    /// <inheritdoc />
    public partial class USUARIO : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DO $$
                BEGIN
                    IF NOT EXISTS (
                        SELECT 1 FROM information_schema.columns 
                        WHERE table_name='tb_Usuario' AND column_name='usuario'
                    ) THEN
                        ALTER TABLE tb_Usuario ADD COLUMN usuario text;
                    END IF;
                END $$;");

            // Agregar la columna 'Rutalmagen' solo si no existe
            migrationBuilder.Sql(@"
                DO $$
                BEGIN
                    IF NOT EXISTS (
                        SELECT 1 FROM information_schema.columns 
                        WHERE table_name='tb_Usuario' AND column_name='Rutalmagen'
                    ) THEN
                        ALTER TABLE tb_Usuario ADD COLUMN Rutalmagen text;
                    END IF;
                END $$;");

            // Eliminar las columnas que ya no son necesarias
            migrationBuilder.DropColumn(
                name: "Email",
                table: "tb_Usuario");

            migrationBuilder.DropColumn(
                name: "Contraseña",
                table: "tb_Usuario");

            migrationBuilder.DropColumn(
                name: "Reestablecer",
                table: "tb_Usuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
           migrationBuilder.DropColumn(
            name: "usuario",
            table: "tb_Usuario");

        migrationBuilder.DropColumn(
            name: "Rutalmagen",
            table: "tb_Usuario");

        // Si las columnas eliminadas deben volver a ser agregadas, hazlo aquí
        migrationBuilder.AddColumn<string>(
            name: "Email",
            table: "tb_Usuario",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            name: "Contraseña",
            table: "tb_Usuario",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            name: "Reestablecer",
            table: "tb_Usuario",
            nullable: true);
        }
    }
}
