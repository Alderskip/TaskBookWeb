using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskbookServer.Models;

namespace TaskbookServer.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        [MinLength(4)]

        public string username { get; set; }
        [Required]
        [MaxLength(50)]
        [MinLength(8)]
        
        public string password { get; set; }


        [MaxLength(50)]
        [MinLength(2)]
        public string firstName { get; set; }


        [MaxLength(50)]
        [MinLength(2)]
        public string lastName { get; set; }


        public string secondOrFathersName { get; set; }

        [Required]
        [EmailAddress]
        public string email { get; set; }

        public string role { get; set; }

        public int totalStudyTaskPoints { get; set; }
        public string gender { get; set; }
        public string localKey { get; set; }
        public string refreshToken { get; set; }
        public DateTime refreshTokenExpiryTime { get; set; }
        public DateTime registrationTime { get; set; }
        public DateTime lastVisit{ get; set; }
        public ICollection<GroupStudent> StudentGroups { get; set; }
        public IList<UserTask> UserTask { get; set; }
        public IList<UserTrophy> UserTrophies { get; set; }

    } 
}
