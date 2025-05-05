using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InterfaceAdapters_Data.Migrations
{
    /// <inheritdoc />
    public partial class Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CapacidadMaxima",
                table: "Turno",
                newName: "Capacidad");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Capacidad",
                table: "Turno",
                newName: "CapacidadMaxima");
        }
    }
}
