using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GerenciadorEscolar.Migrations
{
    public partial class creatingrelatingbetween1Classto1Student : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClassModelId",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Students_ClassModelId",
                table: "Students",
                column: "ClassModelId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Classes_ClassModelId",
                table: "Students",
                column: "ClassModelId",
                principalTable: "Classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Classes_ClassModelId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_ClassModelId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "ClassModelId",
                table: "Students");
        }
    }
}
