namespace TaskbookServer.Models.TableObj
{
    public class GroupTableRowStudent
    {
        public int Id { get; set; }
        public string groupName { get; set; }
        public string teacherFIO { get; set; }
        public bool taskToDo { get; set; }
        public string groupDesc { get; set; }
        public string enviroment { get; set; }

    }
}
