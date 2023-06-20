using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace TaskbookServer.Migrations
{
    public partial class mig1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "courses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    couseName = table.Column<string>(nullable: true),
                    courseDesc = table.Column<string>(nullable: true),
                    difficulty = table.Column<int>(nullable: false),
                    courseFTPPath = table.Column<string>(nullable: true),
                    courseEnv = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_courses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "studyTaskGroups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    studyTaskGroupName = table.Column<string>(nullable: true),
                    studyTaskGroupCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_studyTaskGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "trophies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Trophy_name = table.Column<string>(nullable: true),
                    TrophyDescribsion = table.Column<string>(nullable: true),
                    TrophyImageName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trophies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    username = table.Column<string>(maxLength: 50, nullable: false),
                    password = table.Column<string>(maxLength: 50, nullable: false),
                    firstName = table.Column<string>(maxLength: 50, nullable: true),
                    lastName = table.Column<string>(maxLength: 50, nullable: true),
                    secondOrFathersName = table.Column<string>(nullable: true),
                    email = table.Column<string>(nullable: false),
                    role = table.Column<string>(nullable: true),
                    totalStudyTaskPoints = table.Column<int>(nullable: false),
                    localKey = table.Column<string>(nullable: true),
                    refreshToken = table.Column<string>(nullable: true),
                    refreshTokenExpiryTime = table.Column<DateTime>(nullable: false),
                    registrationTime = table.Column<DateTime>(nullable: false),
                    lastVisit = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "study_tasks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    taskName = table.Column<string>(nullable: true),
                    taskDesc = table.Column<string>(nullable: true),
                    taskPointValue = table.Column<int>(nullable: false),
                    studyTaskGroupId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_study_tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_study_tasks_studyTaskGroups_studyTaskGroupId",
                        column: x => x.studyTaskGroupId,
                        principalTable: "studyTaskGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "authorizationLogEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    userid = table.Column<int>(nullable: false),
                    AutorizationTime = table.Column<DateTime>(nullable: false),
                    IsSuccesful = table.Column<bool>(nullable: false),
                    CausedException = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_authorizationLogEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_authorizationLogEntities_users_userid",
                        column: x => x.userid,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "courseStudents",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    studentId = table.Column<int>(nullable: false),
                    courseEnv = table.Column<string>(nullable: true),
                    courseId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_courseStudents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_courseStudents_courses_courseId",
                        column: x => x.courseId,
                        principalTable: "courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_courseStudents_users_studentId",
                        column: x => x.studentId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "errorLogEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    userid = table.Column<int>(nullable: false),
                    ErrorTime = table.Column<DateTime>(nullable: false),
                    InFunction = table.Column<string>(nullable: true),
                    CausedException = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_errorLogEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_errorLogEntities_users_userid",
                        column: x => x.userid,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    groupName = table.Column<string>(maxLength: 60, nullable: true),
                    groupDesc = table.Column<string>(maxLength: 1000, nullable: true),
                    groupFtpFullPath = table.Column<string>(maxLength: 1000, nullable: true),
                    groupEnv = table.Column<string>(nullable: true),
                    teacherId = table.Column<int>(nullable: false),
                    gradeSys = table.Column<string>(nullable: true),
                    lastUpdateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Groups_users_teacherId",
                        column: x => x.teacherId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "notifications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    NotificationTheme = table.Column<string>(nullable: true),
                    NotificationText = table.Column<string>(nullable: true),
                    UserToRecieveNotId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_notifications_users_UserToRecieveNotId",
                        column: x => x.UserToRecieveNotId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "registrationLogEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    userid = table.Column<int>(nullable: false),
                    RegistrationTime = table.Column<DateTime>(nullable: false),
                    IsSuccesful = table.Column<bool>(nullable: false),
                    CausedException = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_registrationLogEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_registrationLogEntities_users_userid",
                        column: x => x.userid,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "userTrophies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    userId = table.Column<int>(nullable: false),
                    trophyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userTrophies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_userTrophies_trophies_trophyId",
                        column: x => x.trophyId,
                        principalTable: "trophies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_userTrophies_users_userId",
                        column: x => x.userId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseTasks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    taskId = table.Column<int>(nullable: false),
                    courseId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseTasks_courses_courseId",
                        column: x => x.courseId,
                        principalTable: "courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseTasks_study_tasks_taskId",
                        column: x => x.taskId,
                        principalTable: "study_tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "study_task_results",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    StudyTaskId = table.Column<int>(nullable: true),
                    UserId = table.Column<int>(nullable: true),
                    FullResultLine = table.Column<string>(maxLength: 100000000, nullable: true),
                    lastUpdateTimestamp = table.Column<string>(nullable: true),
                    result = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_study_task_results", x => x.Id);
                    table.ForeignKey(
                        name: "FK_study_task_results_study_tasks_StudyTaskId",
                        column: x => x.StudyTaskId,
                        principalTable: "study_tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_study_task_results_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users_tasks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    UserId = table.Column<int>(nullable: false),
                    TaskId = table.Column<int>(nullable: false),
                    Env = table.Column<string>(nullable: true),
                    Completed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users_tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_tasks_study_tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "study_tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_tasks_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Group_Tasks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    groupId = table.Column<int>(nullable: false),
                    studyTaskId = table.Column<int>(nullable: false),
                    maxTaskPointValueInGroup = table.Column<int>(nullable: false),
                    isMandatory = table.Column<bool>(nullable: false),
                    active = table.Column<bool>(nullable: false),
                    isAutoGraded = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Group_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Group_Tasks_Groups_groupId",
                        column: x => x.groupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Group_Tasks_study_tasks_studyTaskId",
                        column: x => x.studyTaskId,
                        principalTable: "study_tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "invitation_to_groups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    groupId = table.Column<int>(nullable: false),
                    userCreatedId = table.Column<int>(nullable: false),
                    invitationToken = table.Column<string>(nullable: true),
                    creationTime = table.Column<DateTime>(nullable: false),
                    currentAcceptedPeople = table.Column<int>(nullable: false),
                    maxPeopleCount = table.Column<int>(nullable: false),
                    invitationStatus = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_invitation_to_groups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_invitation_to_groups_Groups_groupId",
                        column: x => x.groupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_invitation_to_groups_users_userCreatedId",
                        column: x => x.userCreatedId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Student_Groups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    userId = table.Column<int>(nullable: false),
                    groupId = table.Column<int>(nullable: false),
                    inGroupStatus = table.Column<bool>(nullable: false),
                    gradeInGroup = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student_Groups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Student_Groups_Groups_groupId",
                        column: x => x.groupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Student_Groups_users_userId",
                        column: x => x.userId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "groupGrades",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    studentId = table.Column<int>(nullable: false),
                    groupId = table.Column<int>(nullable: false),
                    groupTaskId = table.Column<int>(nullable: false),
                    IsGraded = table.Column<bool>(nullable: false),
                    pointGradeValue = table.Column<int>(nullable: false),
                    seen = table.Column<bool>(nullable: false),
                    resTime = table.Column<DateTime>(nullable: false),
                    resType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_groupGrades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_groupGrades_Groups_groupId",
                        column: x => x.groupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_groupGrades_Group_Tasks_groupTaskId",
                        column: x => x.groupTaskId,
                        principalTable: "Group_Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_groupGrades_users_studentId",
                        column: x => x.studentId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "recieved_Invitations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    invitationId = table.Column<int>(nullable: false),
                    userRecievedId = table.Column<int>(nullable: false),
                    reactionTime = table.Column<DateTime>(nullable: false),
                    recievedInvitationStatus = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_recieved_Invitations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_recieved_Invitations_invitation_to_groups_invitationId",
                        column: x => x.invitationId,
                        principalTable: "invitation_to_groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_recieved_Invitations_users_userRecievedId",
                        column: x => x.userRecievedId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_authorizationLogEntities_userid",
                table: "authorizationLogEntities",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "IX_courseStudents_courseId",
                table: "courseStudents",
                column: "courseId");

            migrationBuilder.CreateIndex(
                name: "IX_courseStudents_studentId",
                table: "courseStudents",
                column: "studentId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseTasks_courseId",
                table: "CourseTasks",
                column: "courseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseTasks_taskId",
                table: "CourseTasks",
                column: "taskId");

            migrationBuilder.CreateIndex(
                name: "IX_errorLogEntities_userid",
                table: "errorLogEntities",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "IX_Group_Tasks_groupId",
                table: "Group_Tasks",
                column: "groupId");

            migrationBuilder.CreateIndex(
                name: "IX_Group_Tasks_studyTaskId",
                table: "Group_Tasks",
                column: "studyTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_groupGrades_groupId",
                table: "groupGrades",
                column: "groupId");

            migrationBuilder.CreateIndex(
                name: "IX_groupGrades_groupTaskId",
                table: "groupGrades",
                column: "groupTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_groupGrades_studentId",
                table: "groupGrades",
                column: "studentId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_teacherId",
                table: "Groups",
                column: "teacherId");

            migrationBuilder.CreateIndex(
                name: "IX_invitation_to_groups_groupId",
                table: "invitation_to_groups",
                column: "groupId");

            migrationBuilder.CreateIndex(
                name: "IX_invitation_to_groups_userCreatedId",
                table: "invitation_to_groups",
                column: "userCreatedId");

            migrationBuilder.CreateIndex(
                name: "IX_notifications_UserToRecieveNotId",
                table: "notifications",
                column: "UserToRecieveNotId");

            migrationBuilder.CreateIndex(
                name: "IX_recieved_Invitations_invitationId",
                table: "recieved_Invitations",
                column: "invitationId");

            migrationBuilder.CreateIndex(
                name: "IX_recieved_Invitations_userRecievedId",
                table: "recieved_Invitations",
                column: "userRecievedId");

            migrationBuilder.CreateIndex(
                name: "IX_registrationLogEntities_userid",
                table: "registrationLogEntities",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "IX_Student_Groups_groupId",
                table: "Student_Groups",
                column: "groupId");

            migrationBuilder.CreateIndex(
                name: "IX_Student_Groups_userId",
                table: "Student_Groups",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_study_task_results_StudyTaskId",
                table: "study_task_results",
                column: "StudyTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_study_task_results_UserId",
                table: "study_task_results",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_study_tasks_studyTaskGroupId",
                table: "study_tasks",
                column: "studyTaskGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_users_username",
                table: "users",
                column: "username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_tasks_TaskId",
                table: "Users_tasks",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_tasks_UserId",
                table: "Users_tasks",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_userTrophies_trophyId",
                table: "userTrophies",
                column: "trophyId");

            migrationBuilder.CreateIndex(
                name: "IX_userTrophies_userId",
                table: "userTrophies",
                column: "userId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "authorizationLogEntities");

            migrationBuilder.DropTable(
                name: "courseStudents");

            migrationBuilder.DropTable(
                name: "CourseTasks");

            migrationBuilder.DropTable(
                name: "errorLogEntities");

            migrationBuilder.DropTable(
                name: "groupGrades");

            migrationBuilder.DropTable(
                name: "notifications");

            migrationBuilder.DropTable(
                name: "recieved_Invitations");

            migrationBuilder.DropTable(
                name: "registrationLogEntities");

            migrationBuilder.DropTable(
                name: "Student_Groups");

            migrationBuilder.DropTable(
                name: "study_task_results");

            migrationBuilder.DropTable(
                name: "Users_tasks");

            migrationBuilder.DropTable(
                name: "userTrophies");

            migrationBuilder.DropTable(
                name: "courses");

            migrationBuilder.DropTable(
                name: "Group_Tasks");

            migrationBuilder.DropTable(
                name: "invitation_to_groups");

            migrationBuilder.DropTable(
                name: "trophies");

            migrationBuilder.DropTable(
                name: "study_tasks");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "studyTaskGroups");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
