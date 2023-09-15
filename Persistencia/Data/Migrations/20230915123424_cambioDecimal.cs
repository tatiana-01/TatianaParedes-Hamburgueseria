using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistencia.Data.Migrations
{
    /// <inheritdoc />
    public partial class cambioDecimal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "stock",
                table: "Ingredientes",
                type: "decimal(20,5)",
                precision: 20,
                scale: 5,
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double",
                oldPrecision: 20,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "Precio",
                table: "Ingredientes",
                type: "decimal(20,5)",
                precision: 20,
                scale: 5,
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double",
                oldPrecision: 20,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "Precio",
                table: "Hamburguesas",
                type: "decimal(20,5)",
                precision: 20,
                scale: 5,
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double",
                oldPrecision: 20,
                oldScale: 5);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "stock",
                table: "Ingredientes",
                type: "double",
                precision: 20,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,5)",
                oldPrecision: 20,
                oldScale: 5);

            migrationBuilder.AlterColumn<double>(
                name: "Precio",
                table: "Ingredientes",
                type: "double",
                precision: 20,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,5)",
                oldPrecision: 20,
                oldScale: 5);

            migrationBuilder.AlterColumn<double>(
                name: "Precio",
                table: "Hamburguesas",
                type: "double",
                precision: 20,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,5)",
                oldPrecision: 20,
                oldScale: 5);
        }
    }
}
