using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskbookServer.Models.TableObj
{
    public class GroupResultTable
    {
        public User user { get; set; }

        public IList<StudyTask> CompletedTasks { get; set; }

        public IList<StudyTask> InprogressTasks { get; set; }

        public int Raiting { get; set; }

        public DateTime LastVistit { get; set; }
    }
}
