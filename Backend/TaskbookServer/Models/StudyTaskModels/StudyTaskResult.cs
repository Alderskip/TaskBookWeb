using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaskbookServer.Models
{
    public class StudyTaskResult
    {
        [Key]
        public int Id { get; set; }
        public StudyTask StudyTask { get; set; }
        
        public User User { get; set; }
        [MaxLength(100000000)]
        public string FullResultLine { get; set; }
        
        public string lastUpdateTimestamp { get; set; }

        public string result { get; set; }
    }
}
