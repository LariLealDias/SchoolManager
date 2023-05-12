using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GerenciadorEscolar.Migrations
{
    public partial class Creatingrelationship1StudenttomultipleSubjects : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClassModelId",
                table: "Subjects",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_ClassModelId",
                table: "Subjects",
                column: "ClassModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Classes_ClassModelId",
                table: "Subjects",
                column: "ClassModelId",
                principalTable: "Classes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Classes_ClassModelId",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_ClassModelId",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "ClassModelId",
                table: "Subjects");
        }
    }
}
