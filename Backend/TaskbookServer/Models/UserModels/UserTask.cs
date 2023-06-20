using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TaskbookServer.Models
{
    public class UserTask
        //Требуется переработать ? - скорее всего да
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        public int UserId { get; set; }
        public User User { get; set; }
        public int TaskId { get; set; }
        public StudyTask Task { get; set; }
        public string Env { get; set; }
        public bool Completed { get; set; }

    }
}
