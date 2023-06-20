using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TaskbookServer.Models
{
    public class RegistrationLogEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public int userid { get; set; }
        public User user { get; set; }
        public DateTime RegistrationTime { get; set; }
        public bool IsSuccesful { get; set; }
        public string CausedException { get; set; }
    }
}
