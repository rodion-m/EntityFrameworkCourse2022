using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleJournal.Migrations
{
    public partial class MakePhoneVO : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Students_Name_Birthday",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Students",
                newName: "Phone_Value");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Phone_Value",
                table: "Students",
                newName: "Phone");

            migrationBuilder.CreateIndex(
                name: "IX_Students_Name_Birthday",
                table: "Students",
                columns: new[] { "Name", "Birthday" });
        }
    }
}
