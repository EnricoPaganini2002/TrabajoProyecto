using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrabajoProyecto.Migrations
{
    public partial class MigracionTarea3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Eliminar",
                table: "Tareas");

            migrationBuilder.AlterColumn<int>(
                name: "Realizada",
                table: "Tareas",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Realizada",
                table: "Tareas",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<bool>(
                name: "Eliminar",
                table: "Tareas",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
