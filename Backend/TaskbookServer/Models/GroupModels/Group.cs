using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TaskbookServer.Models;

namespace TaskbookServer.Models
{
    public class Group
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MinLength(3)]
        [MaxLength(60)]
        public string groupName { get; set; }
        [MaxLength(1000)]
        public string groupDesc { get; set; }
        [MaxLength(1000)]
        public string groupFtpFullPath { get; set; }
        public string groupEnv { get; set; }
        public User teacher { get; set; }
        public int teacherId { get; set; }
        public string gradeSys { get; set; }
        public DateTime lastUpdateTime { get; set; }
        public IList<GroupStudent> studentGroups { get; set; }
        public virtual IList<GroupTask> groupTasks { get; set; }
        
    }
}
