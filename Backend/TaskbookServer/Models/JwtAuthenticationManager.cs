
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using TaskbookServer.Services;
using System.Security.Cryptography;
/*
* @SELECT EXISTS(SELECT * FROM empl1 WHERE username = " +
"\'" + username + "\' AND password = \'" + password + "\') "
*/

namespace TaskbookServer.Models
{
    public class JwtAuthenticationManager
    {
        private readonly string key;
        private readonly IConfiguration _configuration;
        private readonly UserUtilityService _userUtilityService;
        
        public JwtAuthenticationManager(string key, IConfiguration configuration)
        {
            this.key = key;
            _configuration = configuration;
            _userUtilityService = new UserUtilityService(_configuration);
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false, //you might want to validate the audience and issuer depending on your use case
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("The savest and the most non-ironical key in the world ")),
                ValidateLifetime = false,
                
                
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");
            return principal;
        }
        public string Authenticate(string username, string password,bool refresh)
        {
            bool userexist = false;
            User user;
            using (Primary_db_contex db = new Primary_db_contex(_configuration))
            {
                userexist = db.users.Any(x => x.username == username && (x.password == _userUtilityService.GetHashString(password)));
                if (refresh)
                {
                    user = db.users.Where(x => x.username == username && x.password == password).FirstOrDefault();
                }
                else
                    user = db.users.Where(x => x.username == username && x.password == _userUtilityService.GetHashString(password)).FirstOrDefault();
                
            }
            try
            {
                if (userexist|| refresh)
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var tokenKey = Encoding.ASCII.GetBytes(key);
                    var tokeDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                    new Claim(ClaimTypes.Name, user.username),
                    new Claim(ClaimTypes.Role, user.role)
                        }),
                        Expires = DateTime.UtcNow.AddMinutes(30),
                        SigningCredentials =
                        new SigningCredentials
                        (new SymmetricSecurityKey(tokenKey),
                        SecurityAlgorithms.HmacSha256)

                    };
                    
                    using (Primary_db_contex db = new Primary_db_contex(_configuration))
                    {
                        db.authorizationLogEntities.Add(new AuthorizationLogEntity { AutorizationTime = DateTime.Now, IsSuccesful = true, userid = user.Id });
                        db.SaveChanges();
                    }
                    var token = tokenHandler.CreateToken(tokeDescriptor);
                    return tokenHandler.WriteToken(token);
                }
                else
                {

                    return null;
                }
            }
            catch (Exception ex)
            {
                using (Primary_db_contex db = new Primary_db_contex(_configuration))
                {
                    db.authorizationLogEntities.Add(new AuthorizationLogEntity { AutorizationTime = DateTime.Now, IsSuccesful = false, userid = user.Id, CausedException = ex.Message });
                    db.SaveChanges();
                }
                return null;
            }
            



        }

    }
}
