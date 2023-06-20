using FluentFTP;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskbookServer.Models;
using TaskbookServer.Models.CourseModels;
using TaskbookServer.Services;

namespace TaskbookServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CourseController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private readonly ResultService _resultService;
        private readonly UserUtilityService _userUtilityService;
        private readonly RoleService _roleService;
        private readonly TimeService _timeService;
        private readonly GroupService _groupService;
        private readonly AccessFileGeneratorS _accessService;
        private readonly TaskService _taskService;
        public CourseController(IConfiguration configuration)
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
        }
        public string[] enviroments = "C# Python3 C++".Split(' ');
        Dictionary<string, string[]> coursesInfo =
            new Dictionary<string, string[]> {
                { "BeginCourse", new string[]{"Курс для тех, кто совершенно не знаком с программированием","0" } },
                { "ForCourse", new string[]{"Курс для тех, кто хочет разобраться с оператором for или узнать о нем что-то новое","1" }  },
                { "WhileCourse", new string[]{ "Курс для тех, кто хочет разобраться с оператором while или узнать о нем что-то новое", "1" }  },
                { "BooleanCourse",new string[]{ "Курс для тех, кто хочет разобраться с типом boolean или узнать о нем что-то новое", "0" }  },
                { "IntegerCourse",new string[]{ "Курс для тех, кто хочет разобраться с типом integer или узнать о нем что-то новое", "0" }  },
                { "IfCourse",new string[]{ "Курс для тех, кто хочет разобраться с оператором if или узнать о нем что-то новое", "1" }  },
                { "CaseCourse",new string[]{ "Курс для тех, кто хочет разобраться с оператором case или узнать о нем что-то новое", "1" }  },

            };


        public async Task GenerateCourseFtpDirectory(string path)
        {
            try
            {
                using (FtpClient ftp = new FtpClient(_configuration["FtpData:ftpHost"],
                    new System.Net.NetworkCredential
                    {
                        UserName = _configuration["FtpData:ftpLogin"],
                        Password = _configuration["FtpData:ftpPassword"]
                    }))
                {
                    await ftp.AutoConnectAsync();
                    if (!await ftp.DirectoryExistsAsync(path))
                    {

                        bool a = await ftp.CreateDirectoryAsync(path, true);

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        [AllowAnonymous]
        [HttpPost("CouseCreation")]
        public async Task<IActionResult> PostCourses2()
        {
            using (Primary_db_contex db = new Primary_db_contex(_configuration))
            {
                List<Course> courses = new List<Course>();
                foreach (var enviroment in enviroments)
                {
                    foreach (var courseInfo in coursesInfo)
                    {
                        courses.Add(new Course { courseName = courseInfo.Key, courseDifficulty = int.Parse(courseInfo.Value[1]), courseEnv = enviroment, courseDesc = courseInfo.Value[0], courseFTPPath = "Courses/" + enviroment + "/" + courseInfo.Key });
                        await GenerateCourseFtpDirectory(courses.Last().courseFTPPath);
                    }

                }
                db.courses.AddRange(courses);
                db.SaveChanges();
                courses = db.courses.ToList();
                foreach (var course in courses)
                {
                    List<CourseTask> tasks = new List<CourseTask>();
                    foreach (var elem in db.study_tasks.Where(x => x.studyTaskGroup.studyTaskGroupName == course.courseName.Replace("Course", "")))
                    {
                        tasks.Add(new CourseTask { courseId = course.Id, course = course, task = elem, taskId = elem.Id });
                    }
                    db.CourseTasks.AddRange(tasks);
                    db.SaveChanges();

                }
                return Ok();
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetCouses([FromQuery] string env)
        {
            try
            {
                using (Primary_db_contex db = new Primary_db_contex(_configuration))
                {
                    if (env == "C1")
                        env = "C++";
                    if (env == "C")
                        env = "C#";

                    return Ok(await db.courses.Where(x=>x.courseEnv==env).ToListAsync());
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }
        [HttpGet("GetUserCourses")]
        public async Task<IActionResult> GetUserCourses()
        {
            var t = Request.Headers["Authorization"].ToString();
            try
            {
                List<Course> courses = new List<Course>();
                using (Primary_db_contex db = new Primary_db_contex(_configuration))
                {
                    foreach (var courseStudentRelation in db.courseStudents.
                        Where(x => x.userId == _userUtilityService.GetUserObjectFromToken(t).Id).Include(x=>x.course))
                    {
                        courses.Add(courseStudentRelation.course);
                    }
                    return Ok(courses);

                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }
        [HttpPost("AddStudentToCourse")]
        public async Task<IActionResult> AddStudentToCourse([FromBody] CourseStudent s)
        {
            var t = Request.Headers["Authorization"].ToString();
            s.userId = _userUtilityService.GetUserObjectFromToken(t).Id;
            using (Primary_db_contex db = new Primary_db_contex(_configuration))
            {

                if (!await db.courseStudents.AnyAsync(x => x.userId == s.userId && x.courseId == s.courseId))
                {
                    await db.courseStudents.AddAsync(new Models.CourseModels.CourseStudent { userId = s.userId, courseId = s.courseId });
                    await db.SaveChangesAsync();
                    return Ok("Успешно");
                }
                else
                    return Ok("Вы уже записаны ");


            }
        }
        [HttpGet("GetCourseTasks")]
        public async Task<IActionResult> GetCourseTasks([FromQuery] int courseId)
        {
            
            
                using (Primary_db_contex db = new Primary_db_contex(_configuration))
                {
                    List<StudyTask> res = new List<StudyTask>();
                    foreach (var elem in db.CourseTasks.Where(x => x.courseId == courseId).Include(x => x.task))
                    {
                        res.Add(new StudyTask { taskName = elem.task.taskName, taskDesc = elem.task.taskDesc, taskPointValue = elem.task.taskPointValue });
                    }
                    return Ok(res);
                }
            
           
        }
        
    }
}
