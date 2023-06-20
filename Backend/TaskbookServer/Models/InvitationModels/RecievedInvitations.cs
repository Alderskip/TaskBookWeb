using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TaskbookServer.Models
{
    public class RecievedInvitations
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int invitationId { get; set; }

        public InvitationToGroup invitation{get;set;}

        public int userRecievedId { get; set; }

        public User userRecieved { get; set; }

        public DateTime reactionTime { get; set; }

        public string recievedInvitationStatus { get; set; }
    }

}
