using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TaskbookServer.Models;
using TaskbookServer.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskbookServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonalAccountController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ResultService _resultService;
        private readonly UserUtilityService _userUtilityService;
        private readonly RoleService _roleService;
        private readonly TimeService _timeService;
        private readonly GroupService _groupService;
        private readonly EmailService _emailService;
        public PersonalAccountController(IConfiguration configuration)
        {
            _configuration = configuration;
            _resultService = new ResultService(configuration);
            _userUtilityService = new UserUtilityService(configuration);
            _roleService = new RoleService(configuration);
            _timeService = new TimeService(configuration);
            _groupService = new GroupService(configuration);
            _emailService = new EmailService(configuration);

        }
        [HttpGet("PersonalAccountGeneralUserInfo")] //Api Запрос возращающий данные для формирования личного кабинета пользователя
        public IActionResult PersonalAccoutData()
        {
            var t = Request.Headers["Authorization"].ToString();

            try
            {
                return Ok(_userUtilityService.GetUserObjectFromToken(t));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }
        [HttpPut("PersonalAccountChangeGeneralInfo")] //Api запрос для смены имени или фамилии
        public IActionResult ChangePersonalAccoutData([FromBody] UsecCred UserNewGeneralInfo)
        {
            var t = Request.Headers["Authorization"].ToString();

            var user = _userUtilityService.GetUserObjectFromToken(t);
            try
            {
                using (Primary_db_contex db = new Primary_db_contex(_configuration))
                {
                    if (UserNewGeneralInfo.firstname != "" && UserNewGeneralInfo.firstname != user.firstName)
                        db.users.Find(user.Id).firstName = UserNewGeneralInfo.firstname;
                    if (UserNewGeneralInfo.lastname != "" && UserNewGeneralInfo.firstname != user.lastName)
                        db.users.Find(user.Id).lastName = UserNewGeneralInfo.lastname;
                    if (UserNewGeneralInfo.SecOrFatName != "" && UserNewGeneralInfo.SecOrFatName != user.secondOrFathersName)
                        db.users.Find(user.Id).secondOrFathersName = UserNewGeneralInfo.SecOrFatName;
                    if (UserNewGeneralInfo.email != "" && UserNewGeneralInfo.email != user.email)
                        db.users.Find(user.Id).email = UserNewGeneralInfo.email;
                    db.SaveChanges();
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            

        }
    }
}
