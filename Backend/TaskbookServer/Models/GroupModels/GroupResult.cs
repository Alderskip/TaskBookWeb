using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TaskbookServer.Models
{
    public class GroupResult
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int studentId { get; set; }
        public User student { get; set; }
        public int groupId { get; set; }
        public Group group { get; set; }
        public int groupTaskId { get; set; }
        public GroupTask groupTask { get; set; }
        public bool IsGraded { get; set; }
        public int pointGradeValue { get; set; }

        public Boolean seen { get; set; }

        public DateTime resTime { get; set; }
        public String resType { get; set; }
    }
}
