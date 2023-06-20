using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TaskbookServer.Models;
using TaskbookServer.Models.CourseModels;

namespace TaskbookServer.Models
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            /*
            string lines = System.IO.File.ReadAllText(@"C:\Users\Admin\Desktop\Tasks\BeginTasks.txt");
            var lines1 = lines.Split('\n');
            List<StudyTask> list = new List<StudyTask> { };
            StudyTaskGroup Begin =new StudyTaskGroup { StudyTaskGroupName = "Begin", StudyTaskGroupCode = "1", Id = 1 };
            for (int i = 0; i < 40; i++)
            {
                string[] a = lines1[i].Trim().Split('°');

                list.Add(new StudyTask { Id = i + 1, task_name = a[0], task_description = a[1].Trim('.').Trim(),taskPointValue=20,StudyTaskGroup= Begin}) ;

            }


            modelBuilder.Entity<StudyTask>().HasData
                (
                   Begin,
                   list
                ) ; 
            */
        }
    }


    public class Primary_db_contex : DbContext
    {
        private readonly IConfiguration _configuration;
        public Primary_db_contex(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql(_configuration.GetConnectionString("Connection_string"));
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseSerialColumns();
            modelBuilder.Entity<User>().HasIndex(x => x.username).IsUnique();
            modelBuilder.Entity<User>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<StudyTaskGroup>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<UserTask>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<StudyTaskResult>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<StudyTask>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Group>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<GroupTask>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<GroupResult>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<InvitationToGroup>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<RecievedInvitations>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<AuthorizationLogEntity>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<RegistrationLogEntity>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<ErrorLogEntity>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<GroupStudent>().HasOne(x => x.user).WithMany(x => x.StudentGroups).HasForeignKey(x => x.userId);
            modelBuilder.Entity<GroupStudent>().HasOne(x => x.group).WithMany(x => x.studentGroups).HasForeignKey(x => x.groupId);
            modelBuilder.Entity<Course>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<CourseTask>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<CourseStudent>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Seed();
        }
        public DbSet<User> users { get; set; }
        public DbSet<StudyTask> study_tasks { get; set; }
        public DbSet<UserTask> Users_tasks { get; set; }
        public DbSet<StudyTaskResult> study_task_results { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupStudent> Student_Groups { get; set; }
        public DbSet<GroupTask> Group_Tasks { get; set; }
        public DbSet<InvitationToGroup> invitation_to_groups { get; set; }
        public DbSet<RecievedInvitations> recieved_Invitations { get; set; }
        public DbSet<Notification>notifications  { get; set; }
        public DbSet<AuthorizationLogEntity> authorizationLogEntities { get; set; }
        public DbSet<RegistrationLogEntity> registrationLogEntities { get; set; }
        public DbSet<ErrorLogEntity> errorLogEntities { get; set; }
        public DbSet<Trophy> trophies { get; set; }
        public DbSet<StudyTaskGroup> studyTaskGroups { get; set; }
        public DbSet<UserTrophy> userTrophies { get; set; }
        public DbSet<GroupResult> groupGrades { get; set; }
        public DbSet<Course> courses { get; set; }
        public DbSet<CourseStudent> courseStudents { get; set; }
        public DbSet<CourseTask> CourseTasks { get; set; }

    }
}
    