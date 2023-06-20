using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TaskbookServer.Models;
using TaskbookServer.Services;


namespace TaskbookServer.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private readonly ResultService _resultService;
        private readonly UserUtilityService _userUtilityService;
        private readonly RoleService _roleService;
        private readonly TimeService _timeService;
        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
            _resultService = new ResultService(configuration);
            _userUtilityService = new UserUtilityService(configuration);
            _roleService = new RoleService(configuration);
            _timeService = new TimeService(configuration);

        }

        //Новые api

        [AllowAnonymous]
        [HttpGet("StudentCount")] //APi для подсчета общего числа студентов 
        public async Task<IActionResult> GetStudentCount()
        {
            using (Primary_db_contex db = new Primary_db_contex(_configuration))
            {
                var response = await db.users.Where(x => x.role != "Admin" && x.role != "Teacher").CountAsync();
                return Ok(response);
            }
        }
        [AllowAnonymous]
        [HttpGet("StudentRaitingTable")]
        public async Task<IActionResult> GetStudentRaitingTable([FromQuery] TaskbookServer.Models.PaginationFilter filter)
        {
            
            using (Primary_db_contex db = new Primary_db_contex(_configuration))
            {
                try
                {
                    return Ok(await db.users.Where(x => x.role == "Student").OrderByDescending(x => x.totalStudyTaskPoints).Take(10).ToListAsync());
                }
                catch (Exception ex)
                {
                    db.errorLogEntities.Add(new ErrorLogEntity
                    {
                        InFunction = "GetStudentRaitingTable",
                        CausedException = ex.Message,
                    });
                    return BadRequest();
                }

            }

        }
        //Cтарые api
        

        [HttpGet]
        public async Task<IActionResult> GetResultTable([FromQuery] TaskbookServer.Models.PaginationFilter filter)  //Api Запрос сортирующий таблицу результатов стандартно( по id)
        {
            var t = Request.Headers["Authorization"].ToString();
            _timeService.LastVisitUpdate(t);
            DataTable table = CreateTable();
            //if (CheckUserRole(t) != "Teacher" || CheckUserRole(t) != "Admin")
            //{
            //  return Unauthorized();
            //}
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);


            using (Primary_db_contex db = new Primary_db_contex(_configuration))
            {
                var dbusers = db.users.OrderByDescending(x => x.Id);
                switch (filter.SortСolumn)
                {
                    case "FIO":
                        if (filter.SortBy == "Desc")
                        {
                            dbusers = dbusers.OrderByDescending(x => x.lastName);
                            break;
                        }
                        dbusers = dbusers.OrderBy(x => x.lastName);
                        break;
                    case "LastVisit":
                        if (filter.SortBy == "Desc")
                        {
                            dbusers = dbusers.OrderByDescending(x => x.lastVisit);
                            break;
                        }
                        dbusers = dbusers.OrderBy(x => x.lastVisit);
                        break;
                    case "Id":
                        if (filter.SortBy == "Desc")
                        {
                            dbusers = dbusers.OrderByDescending(x => x.Id);
                            break;
                        }
                        dbusers = dbusers.OrderBy(x => x.Id);
                        break;
                }
                foreach (var user in dbusers.Where(x => x.role != "Admin" && x.role != "Teacher"))
                {
                    DataRow a = table.NewRow();
                    a["FIO"] = _userUtilityService.GetFIO(user);
                    a["CompletedTasks"] = _resultService.StringOfCompletedTask(user);
                    a["InProcessTasks"] = _resultService.StringOfInprogressTask(user, _resultService.StringOfCompletedTask(user));
                    a["LastVistit"] = user.lastVisit.ToString("dd/MM/yyyy") + " " + user.lastVisit.ToShortTimeString();
                    a["Raiting"] = _resultService.GetRaiting(user, a["CompletedTasks"].ToString(), a["InProcessTasks"].ToString());
                    table.Rows.Add(a);

                }
                switch (filter.SortСolumn)
                {
                    case "Raiting":
                        {
                            DataView d = table.DefaultView;
                            if (filter.SortBy == "Desc")
                            {
                                d.Sort = "Raiting Desc";
                                table = d.ToTable();
                                break;
                            }
                            d.Sort = "Raiting ASC";
                            table = d.ToTable();
                            break;
                        }
                    case "InProcessTasks":
                        {
                            DataView d = table.DefaultView;
                            if (filter.SortBy == "Desc")
                            {
                                d.Sort = "InProcessTasks desc";
                                table = d.ToTable();
                                break;
                            }
                            d.Sort = "InProcessTasks ASC";
                            table = d.ToTable();
                            break;
                        }
                    case "CompletedTasks":
                        {
                            DataView d = table.DefaultView;
                            if (filter.SortBy == "Desc")
                            {
                                d.Sort = "CompletedTasks desc";
                                table = d.ToTable();
                                break;
                            }
                            d.Sort = "CompletedTasks ASC";
                            table = d.ToTable();
                            break;
                        }
                }

            }
            DataTable table1 = CreateTable();

            for (int i = (validFilter.PageNumber); i < (validFilter.PageNumber) + validFilter.PageSize; i++)
            {
                if (i < table.Rows.Count)
                {
                    table1.Rows.Add(table.Rows[i].ItemArray);
                }


            }
            return Ok(table1);
        }


        [HttpGet("GetStudentCompletedTasks")]// APi запрос получения строки с завершенными заданиями ученика
        public async Task<IActionResult> StringOfCompletedTaskHTTP()
        {
            var t = Request.Headers["Authorization"].ToString();
            var b = _userUtilityService.GetUserUsernameFromToken(t);
            _timeService.LastVisitUpdate(t);

            using (Primary_db_contex db = new Primary_db_contex(_configuration))
            {
                var c = await db.users.Where(x => x.username == b).FirstOrDefaultAsync();
                var a = db.study_task_results.Where(x => x.User == c && x.result == "Задание выполнено!");
                string res = "";
                foreach (var item in a)
                {
                    string subsring = item.FullResultLine.Split(" ")[0];
                    if (!res.Contains(subsring))
                        res = res + " " + subsring;
                }
                res = res.Trim();
                return Ok(res);
            }
        }


        [HttpGet("GetStudentInprogressTask")]
        public async Task<IActionResult> StringOfInprogressTaskHttp() //Функция получения строки с заданиями в прогрессе ученика
        {
            var t = Request.Headers["Authorization"].ToString();
            var b = _userUtilityService.GetUserUsernameFromToken(t);
            _timeService.LastVisitUpdate(t);

            using (Primary_db_contex db = new Primary_db_contex(_configuration))
            {

                var c = await db.users.Where(x => x.username == b).FirstOrDefaultAsync();
                var a = db.study_task_results.Where(x => x.User == c && x.result != "Задание выполнено!");
                string CompletedTasks = _resultService.StringOfCompletedTask(c);
                string res = "";
                foreach (var item in a)
                {
                    string subsring = item.FullResultLine.Split(" ")[0];
                    if (!res.Contains(subsring) && !CompletedTasks.Contains(subsring))
                        res = res + " " + subsring;
                }
                res.Trim();

                return Ok(res);
            }
        }


        [HttpGet("{ id}")] //Api Запрос возращающий данные одного пользователя по id
        public IActionResult GetById(int Id)
        {
            using (Primary_db_contex db = new Primary_db_contex(_configuration))
            {
                var result = db.users.Find(Id);
                if (result != null)
                    return Ok(result);
                else
                    return NotFound();

            }
        }


        [HttpGet("Personal Account")] //Api Запрос возращающий данные для формирования личного кабинета пользователя
        public async Task<IActionResult> PersonalAccoutData()
        {
            var t = Request.Headers["Authorization"].ToString();

            var username = _userUtilityService.GetUserUsernameFromToken(t);
            using (Primary_db_contex db = new Primary_db_contex(_configuration))
            {
                var result = await db.users.Where(x => x.username == username).FirstOrDefaultAsync();
                if (result != null)
                    return Ok(result);
                else
                    return NotFound();

            }
        }


        [HttpPut("PAСhangeForLastName")] //Api запрос для смены имени или фамилии
        public async Task<IActionResult> ChangePersonalAccoutData([FromBody] UsecCred user)
        {
            var t = Request.Headers["Authorization"].ToString();

            var username = _userUtilityService.GetUserUsernameFromToken(t);
            if (await PersonalAccoutData() != NotFound())
            {
                using (Primary_db_contex db = new Primary_db_contex(_configuration))
                {
                    if (user.firstname != "")
                         db.users.Where(x => x.username == username).FirstOrDefault().firstName = user.firstname;
                    if (user.lastname != "")
                        db.users.Where(x => x.username == username).FirstOrDefault().lastName = user.lastname;
                    await db.SaveChangesAsync();
                    return Ok();
                }
            }
            else
                return NotFound();
        }
        [HttpGet("GetMe")]
        public async Task<IActionResult> GetMe()
        {
            var t = Request.Headers["Authorization"].ToString();
            using (Primary_db_contex db = new Primary_db_contex(_configuration))
            {
                return Ok(await db.users.FindAsync(_userUtilityService.GetUserObjectFromToken(t).Id));
            }
        }
        [HttpGet("GetUserRoleFromToken")]
        public async Task<IActionResult> GetUserRoleFromToken()
        {
            var t = Request.Headers["Authorization"].ToString();
            return Ok(_roleService.GetUserRole(t));
        }
        [HttpPut("PAСhangeForPassword")] //Api запрос для смены пароля
        public async Task<IActionResult> ChangePasswordPersonalAccoutData([FromBody] UsecCred user)
        {
            var t = Request.Headers["Authorization"].ToString();

            var username = _userUtilityService.GetUserUsernameFromToken(t);
            if (await PersonalAccoutData() != NotFound())
            {
                using (Primary_db_contex db = new Primary_db_contex(_configuration))
                {
                    if (_userUtilityService.GetHashString(user.oldpassword) == db.users.Where(x => x.username == username).FirstOrDefault().password)
                        db.users.Where(x => x.username == username).FirstOrDefault().password =  _userUtilityService.GetHashString( user.password);
                    await db.SaveChangesAsync();
                    return Ok();
                }
            }
            else
                return NotFound();
        }
        [AllowAnonymous]
        [HttpPost] // Api запрос на регистрацию пользователя 
        public async Task<IActionResult> Post(User user)
        {
            if (ModelState.IsValid)
            {

                using (Primary_db_contex db = new Primary_db_contex(_configuration))
                {
                    var exists =await db.users.AnyAsync(x => x.username == user.username);
                    if (exists == true)
                        return BadRequest();
                }
                using (Primary_db_contex db = new Primary_db_contex(_configuration))
                {
                    try
                    {
                        user.lastVisit = DateTime.Now;
                        user.registrationTime = DateTime.Now;
                        user.role = "Student";
                        user.password = _userUtilityService.GetHashString(user.password);
                        await db.users.AddAsync(user);
                        await db.SaveChangesAsync();
                        await db.registrationLogEntities.AddAsync(new RegistrationLogEntity { 
                            userid = user.Id,
                            RegistrationTime = DateTime.Now, 
                            IsSuccesful = true });
                        await db.SaveChangesAsync();
                        return Created("~api/User", new
                        {
                            username = user.username,
                            password = _userUtilityService.GetHashString(user.password.ToString()),
                            first_name = user.firstName,
                            last_name = user.lastName,
                            email = user.email,
                            role = "Student"
                        });
                    }
                    catch (Exception ex)
                    {
                        await db.registrationLogEntities.AddAsync(new RegistrationLogEntity { RegistrationTime = DateTime.Now, CausedException = ex.Message, IsSuccesful = false });
                        db.SaveChangesAsync();
                        return BadRequest();
                    }


                }


            }
            else
            {
                using (Primary_db_contex db = new Primary_db_contex(_configuration))
                {
                    db.registrationLogEntities.Add(new RegistrationLogEntity { RegistrationTime = DateTime.Now, CausedException = "Invalid Registration Data", IsSuccesful = false });
                    db.SaveChanges();
                }

                return BadRequest();
            }


        }
        /*
        [HttpPost("UpdateUserTask")]
        public IActionResult UpdateUserTask([FromQuery] int userId)
        {
            using (Primary_db_contex db = new Primary_db_contex(_configuration))
            {
                foreach (var item in db.study_task_results.Where(x=>x.User==db.users.Find(userId)))
                {
                    if (db.Users_tasks.Any(x => x.User == db.users.Find(userId) && x.Task == db.study_task_results.Find(item.Id)))
                    {
                        if (db.Users_tasks.Where(x => x.User == db.users.Find(userId) && x.Task == item.StudyTask).FirstOrDefault().Completed == false && item.result == "Задание выполнено!")
                        {
                            db.Users_tasks.Where(x => x.User == db.users.Find(userId) && x.Task == db.study_tasks.Find(item.StudyTask.Id)).FirstOrDefault().Completed = true;
                            db.SaveChanges();
                        }

                    }
                    else
                    {

                        db.Users_tasks.Add(new UserTask { User = db.users.Find(userId), Task = db.study_tasks.Find(item.StudyTask.Id), Completed = false });
                        db.SaveChanges();
                    }
                }
            }
            return Ok();
           
        }
        */
        [HttpPut("{id}")] // Api запрос изменяющий данные определенного пользователя
        public IActionResult Put(User user)
        {
            if (ModelState.IsValid)
            {
                using (Primary_db_contex db = new Primary_db_contex(_configuration))
                {

                    user.lastVisit = DateTime.Now;
                    db.users.Update(user);
                    db.SaveChanges();
                    return Ok();


                }
            }
            else
                return BadRequest();

        }

        public DataTable CreateTable() // Функция начального задания таблицы с результатами учеников 
        {
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn { ColumnName = "FIO", DataType = System.Type.GetType("System.String") });
            table.Columns.Add(new DataColumn { ColumnName = "Raiting", DataType = System.Type.GetType("System.Int32") });
            table.Columns.Add(new DataColumn { ColumnName = "CompletedTasks", DataType = System.Type.GetType("System.String") });
            table.Columns.Add(new DataColumn { ColumnName = "InProcessTasks", DataType = System.Type.GetType("System.String") });
            table.Columns.Add(new DataColumn { ColumnName = "LastVistit", DataType = System.Type.GetType("System.String") });

            return table;
        }







    }
    /*
    public DataTable SortTable(DataTable unsortedTable,PaginationFilter filter)
    {

    }
    */


}

/*Неиспользуемое :


*/