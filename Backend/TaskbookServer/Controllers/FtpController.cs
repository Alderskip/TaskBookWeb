using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentFTP;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TaskbookServer.Models;
using TaskbookServer.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskbookServer.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FtpController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ResultService _resultService;
        private readonly UserUtilityService _userUtilityService;
        private readonly RoleService _roleService;
        private readonly TimeService _timeService;
        private readonly GroupService _groupService;
        public FtpController(IConfiguration configuration)
        {
            _configuration = configuration;
            _configuration = configuration;
            _resultService = new ResultService(configuration);
            _userUtilityService = new UserUtilityService(configuration);
            _roleService = new RoleService(configuration);
            _timeService = new TimeService(configuration);
            _groupService = new GroupService(configuration);
        }
      
        [HttpGet]
        public async Task<string[]> FtpGetDirectoriesList()
        {
            FtpTaskResultService ftp = new FtpTaskResultService(_configuration);
            return await ftp.GetAllDirectoriesList();
        }
       
        [HttpGet("{directoryPath}")]
        public async Task<string[]> FtpGetDirectoryList(string directoryPath)
        {
            FtpTaskResultService ftp = new FtpTaskResultService(_configuration);
            return await ftp.GetDirectoryList(directoryPath);
        }
        [HttpGet("Full")]
        public async Task<FtpListItem[]> FtpGetFullDirectoryList(string directoryPath)
        {
            FtpTaskResultService ftp = new FtpTaskResultService(_configuration);
            return await ftp.GetFullDirectoryList(directoryPath);
        }
        
        [HttpPost]
        public async Task FtpCheck()
        {
            FtpTaskResultService ftp = new FtpTaskResultService(_configuration);
            await ftp.FtpCheckAndUpdate();
            
        }
        [HttpPost("AllTeacherGroups")]
        public async Task FtpCheckTeacherGroups()
        {
            FtpTaskResultService ftp = new FtpTaskResultService(_configuration);
            var t = Request.Headers["Authorization"].ToString();
            _timeService.LastVisitUpdate(t);
            string role = _roleService.GetUserRole(t);
            
            User user = _userUtilityService.GetUserObjectFromToken(t);
            using (Primary_db_contex db = new Primary_db_contex(_configuration))
            {
                var b=db.Groups.Where(x => x.teacher.Id == user.Id).ToList();
                foreach (var item in b)
                    await ftp.FtpCheckAndUpdate(item.groupFtpFullPath);
                
            }
           
        }

    }
}
