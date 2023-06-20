using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace TaskbookServer.Models.CourseModels
{
    public class CourseTask
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int taskId { get; set; }
        public StudyTask task { get; set; }
        public int courseId { get; set; }
        public Course course { get; set; }
    }
}
