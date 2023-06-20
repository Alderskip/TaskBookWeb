using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskbookServer.Migrations
{
    public partial class mig4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_courseStudents_users_studentId",
                table: "courseStudents");

            migrationBuilder.DropIndex(
                name: "IX_courseStudents_studentId",
                table: "courseStudents");

            migrationBuilder.DropColumn(
                name: "courseEnv",
                table: "courseStudents");

            migrationBuilder.DropColumn(
                name: "studentId",
                table: "courseStudents");

            migrationBuilder.AddColumn<string>(
                name: "gender",
                table: "users",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "userId",
                table: "courseStudents",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_courseStudents_userId",
                table: "courseStudents",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_courseStudents_users_userId",
                table: "courseStudents",
                column: "userId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_courseStudents_users_userId",
                table: "courseStudents");

            migrationBuilder.DropIndex(
                name: "IX_courseStudents_userId",
                table: "courseStudents");

            migrationBuilder.DropColumn(
                name: "gender",
                table: "users");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "courseStudents");

            migrationBuilder.AddColumn<string>(
                name: "courseEnv",
                table: "courseStudents",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "studentId",
                table: "courseStudents",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_courseStudents_studentId",
                table: "courseStudents",
                column: "studentId");

            migrationBuilder.AddForeignKey(
                name: "FK_courseStudents_users_studentId",
                table: "courseStudents",
                column: "studentId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
