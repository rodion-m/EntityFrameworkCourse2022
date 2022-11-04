using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleJournal.Migrations
{
    public partial class AddingRelationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Visits_StudentId",
                table: "Visits",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Visits_SubjectId",
                table: "Visits",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Visits_Students_StudentId",
                table: "Visits",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Visits_Subjects_SubjectId",
                table: "Visits",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Visits_Students_StudentId",
                table: "Visits");

            migrationBuilder.DropForeignKey(
                name: "FK_Visits_Subjects_SubjectId",
                table: "Visits");

            migrationBuilder.DropIndex(
                name: "IX_Visits_StudentId",
                table: "Visits");

            migrationBuilder.DropIndex(
                name: "IX_Visits_SubjectId",
                table: "Visits");
        }
    }
}
