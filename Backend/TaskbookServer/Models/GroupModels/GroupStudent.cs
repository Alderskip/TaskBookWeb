using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TaskbookServer.Models;

namespace TaskbookServer.Models
{
    public class GroupStudent
    {
        [Key]
        public int Id { get; set; }

        public int userId { get; set; }
        public User user { get; set; }
        public int groupId { get; set; }

        public Group group { get; set; }

        public bool inGroupStatus { get; set; }

        public int gradeInGroup { get; set; }
    }
}
