using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using TaskbookServer.Models;

namespace TaskbookServer.Services
{
    public class TimeService
    {
        private readonly IConfiguration _configuration;
        public TimeService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GetLastVistiTime(User user) //Функция перевода времени последнего действия пользователя в привычную форму
        {
            using (Primary_db_contex db = new Primary_db_contex(_configuration))
            {
                return user.lastVisit.ToString("dd / MM / yyyy");
            }
        }
        public void LastVisitUpdate(string token) //Функция обновления времени последнего действия пользователя
        {
            var t1 = token.Split(" ")[1];
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(t1);
            var username = jwtSecurityToken.Claims.First(claim => claim.Type == "unique_name").Value;
            using (Primary_db_contex db = new Primary_db_contex(_configuration))
            {
                db.users.Where(x => x.username == username).FirstOrDefault().lastVisit = DateTime.Now;
                db.SaveChanges();
            }

        }
    }
}
