using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GerenciadorEscolar.Migrations
{
    public partial class adjusting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ClassModels",
                table: "ClassModels");

            migrationBuilder.RenameTable(
                name: "ClassModels",
                newName: "Class");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Class",
                table: "Class",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Class",
                table: "Class");

            migrationBuilder.RenameTable(
                name: "Class",
                newName: "ClassModels");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClassModels",
                table: "ClassModels",
                column: "Id");
        }
    }
}
