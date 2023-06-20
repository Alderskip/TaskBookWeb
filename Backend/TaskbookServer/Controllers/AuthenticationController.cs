using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TaskbookServer.Models;
using TaskbookServer.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskbookServer.Controllers
{
    [Authorize]
    [Route("api")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        public class responseUserData
        {
            public int Id;
            public string username;
            public string password;
            public string email;
            public string firstName;
            public string lastName;
            public string secondOrFathersName;
            public string role;
            public string gender;
        }

        private readonly JwtAuthenticationManager jwtAuthenticationManager;
        private readonly Primary_db_contex _dbContex;
        private readonly UserUtilityService _userUtilityService;
        private readonly TimeService _timeService;
        private readonly IConfiguration _configuration;
        public AuthenticationController(JwtAuthenticationManager jwtAuthenticationManager, IConfiguration configuration)
        {
            this._userUtilityService = new UserUtilityService(configuration);
            this.jwtAuthenticationManager = jwtAuthenticationManager;
            this._timeService = new TimeService(configuration);
            _configuration = configuration;
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Authenticate([FromBody] UsecCred usecCred)
        {
            
            if (usecCred == null)
                return BadRequest();
            using (Primary_db_contex db = new Primary_db_contex(_configuration))
            {
                var user = db.users.FirstOrDefault(x=>x.username==usecCred.username && x.password==_userUtilityService.GetHashString(usecCred.password));
                if (user == null)
                    return Unauthorized();
                var token = jwtAuthenticationManager.Authenticate(usecCred.username, usecCred.password,false);
                var refreshToken = jwtAuthenticationManager.GenerateRefreshToken();
                user.refreshToken = refreshToken;
                user.refreshTokenExpiryTime = DateTime.Now.AddDays(7);
                if (token == null)
                    return Unauthorized();
                await db.SaveChangesAsync();
                return Ok(new AuthenticatedResponse
                {
                    token = token,
                    RefreshToken = refreshToken,
                    user = new responseUserData {username=user.username,
                        password=user.password,
                        email=user.email,
                        secondOrFathersName=user.secondOrFathersName,
                        Id=user.Id,
                        firstName=user.firstName,
                        lastName=user.lastName,
                        role=user.role,
                        gender=user.gender,
                    }
                }) ;

            }
           
            
            
        }
        [HttpGet]
        [Route("refresh")]
        public async Task<IActionResult> Refresh()
        {
            using (Primary_db_contex db = new Primary_db_contex(_configuration))
            {
                
                string accessToken = Request.Headers["authorization"].ToString().Split(" ")[1];
                var t = Request.Headers["Authorization"].ToString();
                _timeService.LastVisitUpdate(t);
                var principal = jwtAuthenticationManager.GetPrincipalFromExpiredToken(accessToken);
                var username = principal.Identity.Name; //this is mapped to the Name claim by default
                var user = db.users.SingleOrDefault(u => u.username == username);
                if (user is null ||  user.refreshTokenExpiryTime <= DateTime.Now)
                    return BadRequest("Invalid client request");
                var newAccessToken = jwtAuthenticationManager.Authenticate(user.username,user.password,true);
                var newRefreshToken = jwtAuthenticationManager.GenerateRefreshToken();
                user.refreshToken = newRefreshToken;
                await db.SaveChangesAsync();
                return Ok(new AuthenticatedResponse()
                {
                    token = newAccessToken,
                    RefreshToken = newRefreshToken,
                    user = new responseUserData
                    {
                        username = user.username,
                        password = user.password,
                        email = user.email,
                        secondOrFathersName = user.secondOrFathersName,
                        Id = user.Id,
                        firstName = user.firstName,
                        lastName = user.lastName,
                        role = user.role,
                        gender=user.gender,

                    }
                }) ;
            }
                
        }
        [HttpPost, Authorize]
        [Route("revoke")]
        public async Task<IActionResult> Revoke()
        {
            using (Primary_db_contex db = new Primary_db_contex(_configuration))
            {
                var username = User.Identity.Name;
                var user = db.users.SingleOrDefault(u => u.username == username);
                if (user == null) return BadRequest();
                user.refreshToken = null;
                await db.SaveChangesAsync();
                return Ok();
            }
                
        }
        [HttpPost("checkUsernameValid"), AllowAnonymous]
        public async Task<IActionResult> checkUsernameValid([FromBody]UsecCred user)
        {
            using (Primary_db_contex db = new Primary_db_contex(_configuration))
            {
                if (db.users.Any(x => x.username == user.username) || user.username.Length<=4)
                    return Ok("NotValid");
                else
                    return Ok("valid");
            }

        }
        [HttpGet]
        public async Task<IActionResult> CheckAuthenticationStatus()
        {
            return Ok(true);
        }





    }
       
}
