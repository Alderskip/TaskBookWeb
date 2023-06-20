using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TaskbookServer.Models;
using TaskbookServer.Models.CourseModels;
using TaskbookServer.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskbookServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ResultService _resultService;
        private readonly UserUtilityService _userUtilityService;
        private readonly RoleService _roleService;
        private readonly TimeService _timeService;
        private readonly GroupService _groupService;
        private readonly AccessFileGeneratorS _accessService;
        private readonly TaskService _taskService;
        private readonly EmailService _emailService;
        public AdminController(IConfiguration configuration)
        {
            _configuration = configuration;
            _configuration = configuration;
            _resultService = new ResultService(configuration);
            _userUtilityService = new UserUtilityService(configuration);
            _roleService = new RoleService(configuration);
            _timeService = new TimeService(configuration);
            _groupService = new GroupService(configuration);
            _accessService = new AccessFileGeneratorS(configuration);
            _taskService = new TaskService(configuration);
            _emailService = new EmailService(configuration);
        }


        [AllowAnonymous]
        [HttpPost("InitialTaskDbCreation")]
        public IActionResult InitialTaskDbCreation()
        {
            try
            {
               _taskService.parseStudyTaskGroup
               (
                   "BeginTasksHTML.txt",
                   "Begin",
                   40,
                   new Dictionary<string, int[]> {
                    {"Primary", new int[]{ 3, 9, 10, 13, 17, 22, 27 } },
                    {"Secondary", new int[]{ 4, 7, 8, 11, 14, 18, 23, 28 } }
                    
                   },
                   new Dictionary<string, int[]>
                   {
                    {"Easy", new int[]{ 1, 2, 8, 16 } },
                    {"Hard", new int[]{ 19, 21, 39, 40 } }
                   },
                   5
               );
               
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
           
        }
        [HttpPost("SendTestEmail")]
        public async Task<IActionResult> SendTestEmail()
        {
             EmailService.SendEmail("","").Wait();
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("InitialTaskDbCreation2")]
        public IActionResult PostTasks2()
        {
            if (Request.Headers["AdminKey"].ToString() != "12345")
                return Unauthorized();
            var elem = System.IO.Directory.GetFiles(@"../Tasks/");
            foreach (var elem1 in elem)
            {
                string[] liness = System.IO.File.ReadAllText(elem1).Split("\n");
                string studyTaskName = elem1.Replace("Tasks", "").Replace("..//","").Replace(".txt","");
                StudyTaskGroup TaskGroup = new StudyTaskGroup{studyTaskGroupName=studyTaskName,studyTaskGroupCode="" };
                string ToWrite = "";
                int x = 0;
                using (Primary_db_contex db = new Primary_db_contex(_configuration))
                {

                    db.studyTaskGroups.Add(TaskGroup);
                    db.SaveChanges();

                }

                foreach (var line in liness)
                {
                    x++;
                    if (line == "")
                        continue;
                    if (line.Contains(studyTaskName ) && ToWrite != "")
                    {
                        using (Primary_db_contex db = new Primary_db_contex(_configuration))
                        {
                            string[] a = ToWrite.Split(" ", 2);
                            a[0] = a[0].Trim().Replace("°", "").Replace(".", "");
                            a[1] = a[1].Trim();
                            db.study_tasks.Add(new StudyTask {
                                taskName = a[0],
                                taskDesc = a[1].Trim(),
                                taskPointValue = _taskService.GetStudyTaskPoint(studyTaskName)+x,
                                studyTaskGroup = db.studyTaskGroups.Where(x => x.studyTaskGroupName == studyTaskName).FirstOrDefault()
                            });
                            db.SaveChanges();
                        }
                        ToWrite = "";
                    }
                    ToWrite += line;

                    
                        
                }
            }

            return Ok();
        }
        [HttpPost("GreateGroupAdmin")]
        public IActionResult GreateGroupAdmin([FromQuery] int teacherId, string groupName, string groupEnv)
        {

            if (Request.Headers["AdminKey"].ToString() != "12345")
                return Unauthorized();
            using (Primary_db_contex db = new Primary_db_contex(_configuration))
            {
                db.Groups.Add(new Group { teacherId = teacherId, groupName = groupName, groupEnv = groupEnv });
                db.SaveChanges();
            }
            return Ok();
        }
        [HttpPost("AddStudentToGroupAdmin")]
        public IActionResult AddStudentToGroupAdmin([FromQuery] int userId, int groupId)
        {
            if (Request.Headers["AdminKey"].ToString() != "12345")
                return Unauthorized();
            using (Primary_db_contex db = new Primary_db_contex(_configuration))
            {
                db.Student_Groups.Add(new GroupStudent { groupId = groupId, userId = userId });
                UserUtilityService.CreateNewFtpDirectoryPathOnAdd(groupId, userId, _configuration);
                db.SaveChanges();
            }
            return Ok();
        }
        [HttpPost("AddTestPeopleAdmin")]
        public IActionResult AddTestPeopleAdmin()
        {
            if (Request.Headers["AdminKey"].ToString() != "12345")
                return Unauthorized();
            using (Primary_db_contex db = new Primary_db_contex(_configuration))
            {
                db.users.Add(new User
                {
                    firstName = "Иван",
                    lastName = "Иванов",
                    secondOrFathersName = "Иванович",
                    username = "superVano",
                    password = _userUtilityService.GetHashString("password"),
                    role = "Student",
                    registrationTime = DateTime.UtcNow,
                    email = "superVanoemail@mail.ru",

                });
                _userUtilityService.CreateFTPdirectory("superVano");
                db.users.Add(new User
                {
                    firstName = "Петр",
                    lastName = "Романов",
                    secondOrFathersName = "Петрович",
                    username = "PetrThefirst",
                    password = _userUtilityService.GetHashString("password2"),
                    role = "Student",
                    registrationTime = DateTime.UtcNow,
                    email = "PetrThefirstemail@mail.ru",

                });
                _userUtilityService.CreateFTPdirectory("PetrThefirst");
                db.users.Add(new User
                {
                    firstName = "Геннадий",
                    lastName = "Васильков",
                    secondOrFathersName = "Михмйлович",
                    username = "GennaVasilkov",
                    password = _userUtilityService.GetHashString("password3"),
                    role = "Teacher",
                    registrationTime = DateTime.UtcNow,
                    email = "GennaVasilkov@mail.ru",

                });
                _userUtilityService.CreateFTPdirectory("GennaVasilkov");
                db.users.Add(new User
                {
                    firstName = "Дарья",
                    lastName = "Иванова",
                    secondOrFathersName = "Ивановна",
                    username = "superDasha",
                    password = _userUtilityService.GetHashString("password4"),

                    role = "Student",
                    registrationTime = DateTime.UtcNow,
                    email = "superDasha@mail.ru",

                });
                _userUtilityService.CreateFTPdirectory("superDasha");
                db.SaveChanges();
            }
            return Ok();
        }
        [HttpPost("AddTaskToGroupAdmin")]
        public IActionResult AddTaskToGroupAdmin(int groupId, int taskId)
        {
            if (Request.Headers["AdminKey"].ToString() != "12345")
                return Unauthorized();
            using (Primary_db_contex db = new Primary_db_contex(_configuration))
            {
                db.Group_Tasks.Add(new GroupTask { groupId = groupId, studyTaskId = taskId });
                db.SaveChanges();
            }
            return Ok();
        }
        [HttpPost("TestAccessdat")]
        public IActionResult TestAccessdat()
        {
            string username; string cursOrGroup; string cursEnv; string GrRepName; string RepDir; string ftpDir; string ftppsw; string ftplogin; string RepPsw;
            username = "superDasha"; RepDir = "group4ID27"; ftpDir = "testftpservertaskbook.ucoz.net"; ftplogin = "etestftpservertaskbook"; RepPsw = ""; cursEnv = "PYTHON3"; cursOrGroup = "group4ID27/superDasha"; GrRepName = cursOrGroup; ftppsw = "12345678Aa";
            _accessService.WriteAccessDatFile(username, cursOrGroup, cursEnv, GrRepName, RepDir, ftpDir, ftppsw, ftplogin, RepPsw);
            return Ok();
        }
        [HttpGet("GetFile")]
        public async Task<IActionResult> TestFileDowland([FromQuery] int userId, int groupId)
        {
            using (var db = new Primary_db_contex(_configuration))
            {
                User user = await db.users.FindAsync(userId);
                Group group = await db.Groups.FindAsync(groupId);
                byte[] res = _accessService.WriteAccessDatFile(
                    user.username,
                    group.groupName,
                    group.groupEnv,
                    "/" + group.groupName + "ID" + group.Id.ToString(),
                    user.username,
                    "testftpservertaskbook.ucoz.net",
                    "12345678Aa",
                    "etestftpservertaskbook",
                    ""
                    );
                Stream stream = new MemoryStream(res);
                return File(stream, "application/octet-stream", "access.dat");
            }

        }
        [HttpGet("AllUsers")] //APi для адиминистраторов на вывод всех данных о каждом пользователе
        public async Task<IActionResult> GetAllUsers([FromQuery] TaskbookServer.Models.PaginationFilter filter)
        {
            var t = Request.Headers["Authorization"].ToString();
            _timeService.LastVisitUpdate(t);
            var role = _roleService.GetUserRole(t);
            if (role == "Admin")
            {
                var validFilter = new TaskbookServer.Models.PaginationFilter(filter.PageNumber, filter.PageSize);
                using (Primary_db_contex db = new Primary_db_contex(_configuration))
                {
                    var pagedData = await db.users
                .Skip((validFilter.PageNumber - 1))
                .Take(validFilter.PageSize)
                .ToListAsync();
                    var totalRecords = await db.users.CountAsync();
                    return Ok(new PagedResponse<List<User>>(pagedData, validFilter.PageNumber, validFilter.PageSize));
                }
            }
            else
                return Unauthorized();

        }
        [HttpGet("Test")]
        public async Task<IActionResult> test([FromQuery] int userId)
        {
            return Ok(UserUtilityService.UserFtpDir(userId, _configuration));
        }
        [HttpGet("Test2")]
        public async Task<IActionResult> test2([FromQuery] string username, string env, string path)
        {
            byte[] res = _accessService.WriteInitialResultFile("superDasha", "Python3", "C:\\Program Files(x86)\\PT4\\");
            Stream stream = new MemoryStream(res);
            return File(stream, "application/octet-stream", "results.dat");
        }

    }
}
