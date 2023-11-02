using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrabajoProyecto.Migrations
{
    public partial class MigracionTareas2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tareas_Profesores_ProfesorID",
                table: "Tareas");

            migrationBuilder.AlterColumn<int>(
                name: "ProfesorID",
                table: "Tareas",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Tareas_Profesores_ProfesorID",
                table: "Tareas",
                column: "ProfesorID",
                principalTable: "Profesores",
                principalColumn: "ProfesorID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tareas_Profesores_ProfesorID",
                table: "Tareas");

            migrationBuilder.AlterColumn<int>(
                name: "ProfesorID",
                table: "Tareas",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tareas_Profesores_ProfesorID",
                table: "Tareas",
                column: "ProfesorID",
                principalTable: "Profesores",
                principalColumn: "ProfesorID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
