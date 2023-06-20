using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskbookServer.Migrations
{
    public partial class mig2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "difficulty",
                table: "courses");

            migrationBuilder.AddColumn<string>(
                name: "studyTaskType",
                table: "study_tasks",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "courseDifficulty",
                table: "courses",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "studyTaskType",
                table: "study_tasks");

            migrationBuilder.DropColumn(
                name: "courseDifficulty",
                table: "courses");

            migrationBuilder.AddColumn<int>(
                name: "difficulty",
                table: "courses",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
