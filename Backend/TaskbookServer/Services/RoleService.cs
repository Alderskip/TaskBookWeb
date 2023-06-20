using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using TaskbookServer.Models;

namespace TaskbookServer.Services
{
    public class RoleService
    {
        private readonly IConfiguration _configuration;
        public RoleService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GetUserRole(string token) //Проверка роли пользователя
        {
            var handler = new JwtSecurityTokenHandler();
            var t1 = token.Split(" ")[1];
            try
            {
                var jwtSecurityToken = handler.ReadJwtToken(t1);
                return jwtSecurityToken.Claims.First(claim => claim.Type == "role").Value.Replace("\n", "").Replace("\r", "");
            }
            catch (ArgumentException)
            {
                throw new Exception("Invalid token");
            }


        }
        public bool CheckTeacher(string token)
        {
            if (GetUserRole(token) != "Student")
            {
                return true;
            }
            else
                return false;
        }
        public bool CheckStudent(string token)
        {
            if (GetUserRole(token) != "Teacher")
            {
                return true;
            }
            else
                return false;
        }
        public string GetUserRoleFromId(int id)
        {
            using (Primary_db_contex db = new Primary_db_contex(_configuration))
            {
                return db.users.Find(id).role.Replace("\n", "").Replace("\r", "");
            }
        }
    }
}
