using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TaskbookServer.Models
{
    public class Trophy
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Trophy_name { get; set; }

        public string TrophyDescribsion { get; set; }
        public string TrophyImageName { get; set; }
        public IList<UserTrophy> UserTrophies { get; set; }
    }
}
