using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskbookServer.Models
{
    public class InvitationToGroup
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int groupId { get; set; }
        public Group group { get; set; }
        public int userCreatedId { get; set; }
        public User userCreated { get; set; }

        public string invitationToken { get; set; }

        public DateTime creationTime { get; set; }
        public int currentAcceptedPeople { get; set; }
        public int maxPeopleCount    { get; set; }

        public string invitationStatus { get; set; }
    }
}
