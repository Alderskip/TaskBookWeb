﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TaskbookServer.Models;

namespace TaskbookServer.Migrations
{
    [DbContext(typeof(Primary_db_contex))]
    [Migration("20220831025920_mig3")]
    partial class mig3
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "3.1.18")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("TaskbookServer.Models.AuthorizationLogEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("AutorizationTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("CausedException")
                        .HasColumnType("text");

                    b.Property<bool>("IsSuccesful")
                        .HasColumnType("boolean");

                    b.Property<int>("userid")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("userid");

                    b.ToTable("authorizationLogEntities");
                });

            modelBuilder.Entity("TaskbookServer.Models.CourseModels.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<string>("courseDesc")
                        .HasColumnType("text");

                    b.Property<int>("courseDifficulty")
                        .HasColumnType("integer");

                    b.Property<string>("courseEnv")
                        .HasColumnType("text");

                    b.Property<string>("courseFTPPath")
                        .HasColumnType("text");

                    b.Property<string>("courseName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("courses");
                });

            modelBuilder.Entity("TaskbookServer.Models.CourseModels.CourseStudent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<string>("courseEnv")
                        .HasColumnType("text");

                    b.Property<int>("courseId")
                        .HasColumnType("integer");

                    b.Property<int>("studentId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("courseId");

                    b.HasIndex("studentId");

                    b.ToTable("courseStudents");
                });

            modelBuilder.Entity("TaskbookServer.Models.CourseModels.CourseTask", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<int>("courseId")
                        .HasColumnType("integer");

                    b.Property<int>("taskId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("courseId");

                    b.HasIndex("taskId");

                    b.ToTable("CourseTasks");
                });

            modelBuilder.Entity("TaskbookServer.Models.ErrorLogEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("CausedException")
                        .HasColumnType("text");

                    b.Property<DateTime>("ErrorTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("InFunction")
                        .HasColumnType("text");

                    b.Property<int>("userid")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("userid");

                    b.ToTable("errorLogEntities");
                });

            modelBuilder.Entity("TaskbookServer.Models.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<string>("gradeSys")
                        .HasColumnType("text");

                    b.Property<string>("groupDesc")
                        .HasColumnType("character varying(1000)")
                        .HasMaxLength(1000);

                    b.Property<string>("groupEnv")
                        .HasColumnType("text");

                    b.Property<string>("groupFtpFullPath")
                        .HasColumnType("character varying(1000)")
                        .HasMaxLength(1000);

                    b.Property<string>("groupName")
                        .HasColumnType("character varying(60)")
                        .HasMaxLength(60);

                    b.Property<DateTime>("lastUpdateTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("teacherId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("teacherId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("TaskbookServer.Models.GroupResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<bool>("IsGraded")
                        .HasColumnType("boolean");

                    b.Property<int>("groupId")
                        .HasColumnType("integer");

                    b.Property<int>("groupTaskId")
                        .HasColumnType("integer");

                    b.Property<int>("pointGradeValue")
                        .HasColumnType("integer");

                    b.Property<DateTime>("resTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("resType")
                        .HasColumnType("text");

                    b.Property<bool>("seen")
                        .HasColumnType("boolean");

                    b.Property<int>("studentId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("groupId");

                    b.HasIndex("groupTaskId");

                    b.HasIndex("studentId");

                    b.ToTable("groupGrades");
                });

            modelBuilder.Entity("TaskbookServer.Models.GroupStudent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<int>("gradeInGroup")
                        .HasColumnType("integer");

                    b.Property<int>("groupId")
                        .HasColumnType("integer");

                    b.Property<bool>("inGroupStatus")
                        .HasColumnType("boolean");

                    b.Property<int>("userId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("groupId");

                    b.HasIndex("userId");

                    b.ToTable("Student_Groups");
                });

            modelBuilder.Entity("TaskbookServer.Models.GroupTask", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<bool>("active")
                        .HasColumnType("boolean");

                    b.Property<int>("groupId")
                        .HasColumnType("integer");

                    b.Property<bool>("isAutoGraded")
                        .HasColumnType("boolean");

                    b.Property<bool>("isMandatory")
                        .HasColumnType("boolean");

                    b.Property<int>("maxTaskPointValueInGroup")
                        .HasColumnType("integer");

                    b.Property<int>("studyTaskId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("groupId");

                    b.HasIndex("studyTaskId");

                    b.ToTable("Group_Tasks");
                });

            modelBuilder.Entity("TaskbookServer.Models.InvitationToGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<DateTime>("creationTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("currentAcceptedPeople")
                        .HasColumnType("integer");

                    b.Property<int>("groupId")
                        .HasColumnType("integer");

                    b.Property<string>("invitationStatus")
                        .HasColumnType("text");

                    b.Property<string>("invitationToken")
                        .HasColumnType("text");

                    b.Property<int>("maxPeopleCount")
                        .HasColumnType("integer");

                    b.Property<int>("userCreatedId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("groupId");

                    b.HasIndex("userCreatedId");

                    b.ToTable("invitation_to_groups");
                });

            modelBuilder.Entity("TaskbookServer.Models.Notification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<string>("NotificationText")
                        .HasColumnType("text");

                    b.Property<string>("NotificationTheme")
                        .HasColumnType("text");

                    b.Property<int>("UserToRecieveNotId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserToRecieveNotId");

                    b.ToTable("notifications");
                });

            modelBuilder.Entity("TaskbookServer.Models.RecievedInvitations", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<int>("invitationId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("reactionTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("recievedInvitationStatus")
                        .HasColumnType("text");

                    b.Property<int>("userRecievedId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("invitationId");

                    b.HasIndex("userRecievedId");

                    b.ToTable("recieved_Invitations");
                });

            modelBuilder.Entity("TaskbookServer.Models.RegistrationLogEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("CausedException")
                        .HasColumnType("text");

                    b.Property<bool>("IsSuccesful")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("RegistrationTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("userid")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("userid");

                    b.ToTable("registrationLogEntities");
                });

            modelBuilder.Entity("TaskbookServer.Models.StudyTask", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<int>("studyTaskGroupId")
                        .HasColumnType("integer");

                    b.Property<string>("studyTaskType")
                        .HasColumnType("text");

                    b.Property<string>("taskDesc")
                        .HasColumnType("text");

                    b.Property<string>("taskDifficulty")
                        .HasColumnType("text");

                    b.Property<string>("taskName")
                        .HasColumnType("text");

                    b.Property<int>("taskPointValue")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("studyTaskGroupId");

                    b.ToTable("study_tasks");
                });

            modelBuilder.Entity("TaskbookServer.Models.StudyTaskGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<string>("studyTaskGroupCode")
                        .HasColumnType("text");

                    b.Property<string>("studyTaskGroupName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("studyTaskGroups");
                });

            modelBuilder.Entity("TaskbookServer.Models.StudyTaskResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<string>("FullResultLine")
                        .HasColumnType("text")
                        .HasMaxLength(100000000);

                    b.Property<int?>("StudyTaskId")
                        .HasColumnType("integer");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.Property<string>("lastUpdateTimestamp")
                        .HasColumnType("text");

                    b.Property<string>("result")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("StudyTaskId");

                    b.HasIndex("UserId");

                    b.ToTable("study_task_results");
                });

            modelBuilder.Entity("TaskbookServer.Models.Trophy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<string>("TrophyDescribsion")
                        .HasColumnType("text");

                    b.Property<string>("TrophyImageName")
                        .HasColumnType("text");

                    b.Property<string>("Trophy_name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("trophies");
                });

            modelBuilder.Entity("TaskbookServer.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("firstName")
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.Property<string>("lastName")
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.Property<DateTime>("lastVisit")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("localKey")
                        .HasColumnType("text");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.Property<string>("refreshToken")
                        .HasColumnType("text");

                    b.Property<DateTime>("refreshTokenExpiryTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("registrationTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("role")
                        .HasColumnType("text");

                    b.Property<string>("secondOrFathersName")
                        .HasColumnType("text");

                    b.Property<int>("totalStudyTaskPoints")
                        .HasColumnType("integer");

                    b.Property<string>("username")
                        .IsRequired()
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("username")
                        .IsUnique();

                    b.ToTable("users");
                });

            modelBuilder.Entity("TaskbookServer.Models.UserTask", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<bool>("Completed")
                        .HasColumnType("boolean");

                    b.Property<string>("Env")
                        .HasColumnType("text");

                    b.Property<int>("TaskId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("TaskId");

                    b.HasIndex("UserId");

                    b.ToTable("Users_tasks");
                });

            modelBuilder.Entity("TaskbookServer.Models.UserTrophy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<int>("trophyId")
                        .HasColumnType("integer");

                    b.Property<int>("userId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("trophyId");

                    b.HasIndex("userId");

                    b.ToTable("userTrophies");
                });

            modelBuilder.Entity("TaskbookServer.Models.AuthorizationLogEntity", b =>
                {
                    b.HasOne("TaskbookServer.Models.User", "user")
                        .WithMany()
                        .HasForeignKey("userid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TaskbookServer.Models.CourseModels.CourseStudent", b =>
                {
                    b.HasOne("TaskbookServer.Models.CourseModels.Course", "course")
                        .WithMany("courseStudents")
                        .HasForeignKey("courseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TaskbookServer.Models.User", "student")
                        .WithMany()
                        .HasForeignKey("studentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TaskbookServer.Models.CourseModels.CourseTask", b =>
                {
                    b.HasOne("TaskbookServer.Models.CourseModels.Course", "course")
                        .WithMany("courseTasks")
                        .HasForeignKey("courseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TaskbookServer.Models.StudyTask", "task")
                        .WithMany()
                        .HasForeignKey("taskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TaskbookServer.Models.ErrorLogEntity", b =>
                {
                    b.HasOne("TaskbookServer.Models.User", "user")
                        .WithMany()
                        .HasForeignKey("userid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TaskbookServer.Models.Group", b =>
                {
                    b.HasOne("TaskbookServer.Models.User", "teacher")
                        .WithMany()
                        .HasForeignKey("teacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TaskbookServer.Models.GroupResult", b =>
                {
                    b.HasOne("TaskbookServer.Models.Group", "group")
                        .WithMany()
                        .HasForeignKey("groupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TaskbookServer.Models.GroupTask", "groupTask")
                        .WithMany()
                        .HasForeignKey("groupTaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TaskbookServer.Models.User", "student")
                        .WithMany()
                        .HasForeignKey("studentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TaskbookServer.Models.GroupStudent", b =>
                {
                    b.HasOne("TaskbookServer.Models.Group", "group")
                        .WithMany("studentGroups")
                        .HasForeignKey("groupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TaskbookServer.Models.User", "user")
                        .WithMany("StudentGroups")
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TaskbookServer.Models.GroupTask", b =>
                {
                    b.HasOne("TaskbookServer.Models.Group", "group")
                        .WithMany("groupTasks")
                        .HasForeignKey("groupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TaskbookServer.Models.StudyTask", "studyTask")
                        .WithMany("GroupTasks")
                        .HasForeignKey("studyTaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TaskbookServer.Models.InvitationToGroup", b =>
                {
                    b.HasOne("TaskbookServer.Models.Group", "group")
                        .WithMany()
                        .HasForeignKey("groupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TaskbookServer.Models.User", "userCreated")
                        .WithMany()
                        .HasForeignKey("userCreatedId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TaskbookServer.Models.Notification", b =>
                {
                    b.HasOne("TaskbookServer.Models.User", "UserToRecieveNot")
                        .WithMany()
                        .HasForeignKey("UserToRecieveNotId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TaskbookServer.Models.RecievedInvitations", b =>
                {
                    b.HasOne("TaskbookServer.Models.InvitationToGroup", "invitation")
                        .WithMany()
                        .HasForeignKey("invitationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TaskbookServer.Models.User", "userRecieved")
                        .WithMany()
                        .HasForeignKey("userRecievedId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TaskbookServer.Models.RegistrationLogEntity", b =>
                {
                    b.HasOne("TaskbookServer.Models.User", "user")
                        .WithMany()
                        .HasForeignKey("userid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TaskbookServer.Models.StudyTask", b =>
                {
                    b.HasOne("TaskbookServer.Models.StudyTaskGroup", "studyTaskGroup")
                        .WithMany("studyTasks")
                        .HasForeignKey("studyTaskGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TaskbookServer.Models.StudyTaskResult", b =>
                {
                    b.HasOne("TaskbookServer.Models.StudyTask", "StudyTask")
                        .WithMany()
                        .HasForeignKey("StudyTaskId");

                    b.HasOne("TaskbookServer.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("TaskbookServer.Models.UserTask", b =>
                {
                    b.HasOne("TaskbookServer.Models.StudyTask", "Task")
                        .WithMany()
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TaskbookServer.Models.User", "User")
                        .WithMany("UserTask")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TaskbookServer.Models.UserTrophy", b =>
                {
                    b.HasOne("TaskbookServer.Models.Trophy", "trophy")
                        .WithMany("UserTrophies")
                        .HasForeignKey("trophyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TaskbookServer.Models.User", "user")
                        .WithMany("UserTrophies")
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
