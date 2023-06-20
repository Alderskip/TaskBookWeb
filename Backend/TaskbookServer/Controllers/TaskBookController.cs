using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TaskbookServer.Models;
using TaskbookServer.Services;
using FluentFTP;
using System.IO;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskbookServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskBookController : ControllerBase
    {
        public class SyncKey
        {
            public string key;
            public SyncKey(string key)
            {
                this.key = key;
            }
        }
        
        private readonly IConfiguration _configuration;
        private readonly ResultService _resultService;
        private readonly UserUtilityService _userUtilityService;
        private readonly RoleService _roleService;
        private readonly TimeService _timeService;
        private readonly AccessFileGeneratorS _accessFileGeneratorS;
        public TaskBookController(IConfiguration configuration)
        {
            _configuration = configuration;
            _resultService = new ResultService(configuration);
            _userUtilityService = new UserUtilityService(configuration);
            _roleService = new RoleService(configuration);
            _timeService = new TimeService(configuration);
            _accessFileGeneratorS = new AccessFileGeneratorS(configuration);

        }

        
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
        [HttpPost("GetLocalAppKey")]
        public async Task<IActionResult> GetLocalAppKey()
        {
            var t = Request.Headers["Authorization"].ToString();
            _timeService.LastVisitUpdate(t);
            string key = _userUtilityService.GenerateLocalKey();
            using (Primary_db_contex db = new Primary_db_contex(_configuration))
            {
               db.users.FindAsync(_userUtilityService.GetUserObjectFromToken(t).Id).Result.localKey = _userUtilityService.GetHashString(key);
               await db.SaveChangesAsync();
            }
                return Ok(key);
        }
      
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> SyncLocal([FromQuery] string key)
        {
            string hashKey = _userUtilityService.GetHashString(key);
            using (Primary_db_contex db = new Primary_db_contex(_configuration))
            {
                if (db.users.Any(x => x.localKey == hashKey))
                {
                    User user = await db.users.Where(x => x.localKey == hashKey).FirstOrDefaultAsync();
                    
                    return Ok(UserUtilityService.UserFtpDir(user.Id,_configuration));
                }
                    
                else
                    return BadRequest();
            }
                
        }
        [HttpGet("GetUserLocal")]
        public async Task<IActionResult> GetUserLocal([FromQuery] string key)
        {
            string hashKey = _userUtilityService.GetHashString(key);
            using (Primary_db_contex db = new Primary_db_contex(_configuration))
            {
                User user = await db.users.Where(x => x.localKey == hashKey).FirstOrDefaultAsync();
                if (user!=null)
                {

                    UsecCred res = new UsecCred { firstname = user.firstName, lastname = user.lastName, username = user.username };
                    return Ok(res);
                }
                    
                else
                    return BadRequest();
            }
        }
        [HttpGet("SendAccessFile")]
        public async Task<IActionResult> SendAccessFile([FromQuery] string key,int groupId)
        {
            using (Primary_db_contex db = new Primary_db_contex(_configuration))
            {
                User user = db.users.Where(x=>x.localKey== _userUtilityService.GetHashString(key)).FirstOrDefault() ;
                Group group = await db.Groups.FindAsync(groupId);
                byte[] res = _accessFileGeneratorS.WriteAccessDatFile(
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
                Stream a = new MemoryStream(res);
                return Ok(a);
            }
        }
        [HttpGet("SendInitialResultFile")]
        public async Task<IActionResult> SendInitialResultFile([FromQuery] string key, int groupId, string path)
        {
            using (Primary_db_contex db = new Primary_db_contex(_configuration))
            {
                User user = new User();
                user = await db.users.Where(x => x.localKey == _userUtilityService.GetHashString(key)).FirstOrDefaultAsync();
                Group group = await db.Groups.FindAsync(groupId);
                byte[] res = _accessFileGeneratorS.WriteInitialResultFile(user.username, group.groupEnv, "C:\\Program Files (x86)\\PT4");
                Stream a = new MemoryStream(res);
                return Ok(a);
            }
            
        }


    }
}
