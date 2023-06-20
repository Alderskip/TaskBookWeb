using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TaskbookServer.Models
{
    public class GroupTask
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int groupId { get; set; }
        public  Group group { get; set; }
        public int studyTaskId { get; set; }
        public StudyTask studyTask { get; set; }
        public int maxTaskPointValueInGroup { get; set; }

        public bool isMandatory { get; set; }
        public bool active { get; set; }
        public bool isAutoGraded { get; set; }
    }
}
