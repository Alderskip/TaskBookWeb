using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace TaskbookServer.Models.CourseModels
{
    public class Course
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string courseName { get; set; }

        public string courseDesc { get; set; }
        public int courseDifficulty { get; set; }
        public string courseFTPPath { get; set; }

        public string courseEnv { get; set; }

        public ICollection<CourseStudent> courseStudents { get; set; }
        public ICollection<CourseTask> courseTasks { get; set; }
    }
}
