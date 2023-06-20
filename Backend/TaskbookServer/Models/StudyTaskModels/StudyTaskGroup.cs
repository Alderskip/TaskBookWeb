using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TaskbookServer.Models
{
    public class StudyTaskGroup
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string studyTaskGroupName { get; set; }
        public string studyTaskGroupCode { get; set; }

        public IList<StudyTask> studyTasks { get; set; }


    }
}
