using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace TaskbookServer.Models
{
    public class StudyTask
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string taskName { get; set; }
        public string taskDesc { get; set; }
        
        public int taskPointValue { get; set; }
        public int studyTaskGroupId { get; set; }
        public string studyTaskType { get; set; }
        public string taskDifficulty { get; set; }
        public StudyTaskGroup studyTaskGroup { get; set; }
        public virtual IList<GroupTask> GroupTasks { get; set; }
    }
}
