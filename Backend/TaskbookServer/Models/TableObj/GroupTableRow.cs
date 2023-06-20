using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskbookServer.Models.TableObj
{
    public class GroupTableRow
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public string GropuDescribtion { get; set; }
        public IList<User> Students { get; set; }
        public IList<StudyTask> GroupTasks { get; set; }
        public DateTime LastUpdate { get; set; }

    }
}
