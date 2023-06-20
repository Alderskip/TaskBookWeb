using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskbookServer.Models.CourseModels
{
    public class CourseStudent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int userId { get; set; }

        public User user { get; set; }
        public int courseId { get; set; }
        public Course course { get; set; }
    }
}
