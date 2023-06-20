using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TaskbookServer.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskbookServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ResultService _resultService;
        private readonly UserUtilityService _userUtilityService;
        private readonly RoleService _roleService;
        private readonly TimeService _timeService;
        private readonly GroupService _groupService;
        private readonly EmailService _emailService;
        public NotificationController(IConfiguration configuration)
        {
            _configuration = configuration;
            _resultService = new ResultService(configuration);
            _userUtilityService = new UserUtilityService(configuration);
            _roleService = new RoleService(configuration);
            _timeService = new TimeService(configuration);
            _groupService = new GroupService(configuration);
            _emailService = new EmailService(configuration);

        }
        [HttpGet]
        public IActionResult ResFunction()
        {
            List < UsecCred > list= new List<UsecCred> { };
            UsecCred cred = new UsecCred { username = "Baka", password = "dsa" };
            list.Add(new UsecCred { username = "Abaka" });
            list.Add(new UsecCred { username = "Sobaka" });
            return Ok(cred);
        }
    }
    
}
