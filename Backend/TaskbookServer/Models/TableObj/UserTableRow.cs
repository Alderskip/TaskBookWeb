using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskbookServer.Models.TableObj
{
    public class UserTableRow
    {
        public User User { get; set; }
        public bool InGroup { get; set; }
    }
}
