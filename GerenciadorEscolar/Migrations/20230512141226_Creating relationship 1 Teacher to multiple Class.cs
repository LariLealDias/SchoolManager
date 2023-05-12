using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GerenciadorEscolar.Migrations
{
    public partial class Creatingrelationship1TeachertomultipleClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TeacherModelId",
                table: "Classes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Classes_TeacherModelId",
                table: "Classes",
                column: "TeacherModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Teachers_TeacherModelId",
                table: "Classes",
                column: "TeacherModelId",
                principalTable: "Teachers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Teachers_TeacherModelId",
                table: "Classes");

            migrationBuilder.DropIndex(
                name: "IX_Classes_TeacherModelId",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "TeacherModelId",
                table: "Classes");
        }
    }
}
