using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GerenciadorEscolar.Migrations
{
    public partial class CreatingrelationshipNTeachertoNClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClassSchedules",
                columns: table => new
                {
                    TeacherModelId = table.Column<int>(type: "int", nullable: false),
                    ClassModelId = table.Column<int>(type: "int", nullable: false),
                    ClassTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassSchedules", x => new { x.TeacherModelId, x.ClassModelId });
                    table.ForeignKey(
                        name: "FK_ClassSchedules_Classes_ClassModelId",
                        column: x => x.ClassModelId,
                        principalTable: "Classes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassSchedules_Teachers_TeacherModelId",
                        column: x => x.TeacherModelId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ClassSchedules_ClassModelId",
                table: "ClassSchedules",
                column: "ClassModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClassSchedules");
        }
    }
}
