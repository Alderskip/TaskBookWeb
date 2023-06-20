using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskbookServer.Migrations
{
    public partial class mig3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "couseName",
                table: "courses");

            migrationBuilder.AddColumn<string>(
                name: "taskDifficulty",
                table: "study_tasks",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "courseName",
                table: "courses",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "taskDifficulty",
                table: "study_tasks");

            migrationBuilder.DropColumn(
                name: "courseName",
                table: "courses");

            migrationBuilder.AddColumn<string>(
                name: "couseName",
                table: "courses",
                type: "text",
                nullable: true);
        }
    }
}
