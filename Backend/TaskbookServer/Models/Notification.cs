using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TaskbookServer.Models
{
    public class Notification
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string NotificationTheme { get; set; }
        public string NotificationText { get; set; }
        public int UserToRecieveNotId { get; set; }
        public User UserToRecieveNot { get; set; }
    }
}
