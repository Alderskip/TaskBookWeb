using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TaskbookServer.Models;
using System.Security.Cryptography;
using System.Text;
using FluentFTP;
using System.IO;
using System.Net;

namespace TaskbookServer.Services
{
    public class UserUtilityService
    {
        private readonly IConfiguration _configuration;
        private readonly AccessFileGeneratorS _accessFileGeneratorS;
        public UserUtilityService(IConfiguration configuration)
        {
            _configuration = configuration;
            _accessFileGeneratorS = new AccessFileGeneratorS(configuration);
        }
        public string GetUserUsernameFromToken(string token) //Функция возвращающая ник пользователя из jwt токена 
        {
            var handler = new JwtSecurityTokenHandler();
            var t1 = token.Split(" ")[1];
            try
            {
                var jwtSecurityToken = handler.ReadJwtToken(t1);
                return jwtSecurityToken.Claims.First(claim => claim.Type == "unique_name").Value;
            }
            catch (ArgumentException)
            {
                throw new Exception("Invalid token");
            }


        }
        public List<UserTask> GetUserTask(int userId) //Возвращает список всех заданих где фигурирует пользователь
        {
            using (Primary_db_contex db = new Primary_db_contex(_configuration))
            {
                return db.Users_tasks.Where(x => x.Id == userId).Include(x => x.Task).ToList();
            }

        }
        public List<StudyTask> GetUserCompletedTaskList(int userId) //Возвращает список всех выполненных пользователем заданий
        {
            using (Primary_db_contex db = new Primary_db_contex(_configuration))
            {
                List<StudyTask> result = new List<StudyTask>();
                var a = db.Users_tasks.Where(x => x.User.Id == userId && x.Completed == true).Include(x => x.Task).ToList();
                foreach (var item in a)
                {
                    result.Add(item.Task);
                }
                return result;
            }

        }
        public List<StudyTask> GetUserInProgressTaskList(int userId) //Возвращает список всех заданий в прогрессе пользователя 
        {
            using (Primary_db_contex db = new Primary_db_contex(_configuration))
            {
                List<StudyTask> result = new List<StudyTask>();
                var a = db.Users_tasks.Where(x => x.User.Id == userId && x.Completed != true).Include(x => x.Task).ToList();
                foreach (var item in a)
                {
                    result.Add(item.Task);
                }
                return result;
            }

        }
        public List<Trophy> GetUserTrophies(int userId)     // Возвращает список всех трофеев  пользователя
        {
            List<Trophy> result = new List<Trophy> { };
            using (Primary_db_contex db = new Primary_db_contex(_configuration))
            {
                foreach (var elem in db.userTrophies.Where(x => x.userId == userId))
                {
                    result.Add(elem.trophy);
                }
            }
            return (result);

        }

        public User GetUserFromUserUsername(string username) //Возвращает объект "пользователь"(user) из его никнейма(username)
        {
            using (Primary_db_contex db = new Primary_db_contex(_configuration))
            {
                return db.users.Where(b => b.username == username).FirstOrDefault();
            }

        }
        public User GetUserObjectFromToken(string token) //Функция для получения объекта класса user по его jwt токену, полученному при авторизации
        {
            string username = this.GetUserUsernameFromToken(token);
            using (Primary_db_contex db = new Primary_db_contex(_configuration))
            {
                return db.users.Where(x => x.username == username).FirstOrDefault();
            }
        }
        public string GetHashString(string s)
        {
            //переводим строку в байт-массим  
            byte[] bytes = Encoding.Unicode.GetBytes(s);

            //создаем объект для получения средст шифрования  
            MD5CryptoServiceProvider CSP =
                new MD5CryptoServiceProvider();

            //вычисляем хеш-представление в байтах  
            byte[] byteHash = CSP.ComputeHash(bytes);

            string hash = string.Empty;

            //формируем одну цельную строку из массива  
            foreach (byte b in byteHash)
                hash += string.Format("{0:x2}", b);

            return hash;
        }
        public string GenerateLocalKey() //Генерирует код для локального приложения
        {
            char[] letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz12345678".ToCharArray();
            Random rand = new Random();
            string key = "";
            for (int i = 1; i <= 30; i++)
            {
                int letter_num = rand.Next(0, letters.Length - 1);
                key += letters[letter_num];
            }
            return key;

        }
        public void CreateFTPdirectory(string username) //Создает индивидуальную папку на ftp сервере при регистрации пользователя
        {
            using (FtpClient ftp = new FtpClient("testftpservertaskbook1.ucoz.net", new System.Net.NetworkCredential { UserName = "etestftpservertaskbook1", Password = "12345678Aa" }))
            {
                ftp.Connect();
                ftp.CreateDirectory("Students/" + username, true);

            }
        }

        public string GetFIO(User user) //Функция получения полного ФИО ученика 
        {
            return user.firstName + " " + user.lastName;
        }

       

        public static  List<string> UserFtpDir(int UserId, IConfiguration _configuration)
        {
            List<string> dir = new List<string>();
            User user = new User();
            using (Primary_db_contex db = new Primary_db_contex(_configuration))
            {
                 user =  db.users.Find(UserId);
                
            }
            using (FtpClient ftp = new FtpClient("testftpservertaskbook1.ucoz.net", new System.Net.NetworkCredential { UserName = "etestftpservertaskbook1", Password = "12345678Aa" }))
            {
                ftp.Connect();
                var a =  ftp.GetNameListing();
                foreach (var item in a)
                {
                    var b=ftp.GetNameListing(item);
                    if (b.ToList().Contains("d" + user.username))
                    {
                        dir.Add(item);
                    }
                }
                

            }
            return dir;
        }
        
        public static List<string> UserFTPDirectories(string fullFTPpath)
        {
            using (FtpClient ftp = new FtpClient("testftpservertaskbook1.ucoz.net", new System.Net.NetworkCredential { UserName = "etestftpservertaskbook1", Password = "12345678Aa" }))
            {
                ftp.Connect();
                var a = ftp.GetNameListing(fullFTPpath);
                return a.ToList();
            }
        }

        

        public async static void CreateNewFtpDirectoryPathOnAdd(int groupId, int userId, IConfiguration _configuration)
        {
            using (FtpClient ftp = new FtpClient("testftpservertaskbook1.ucoz.net", new System.Net.NetworkCredential { UserName = "etestftpservertaskbook1", Password = "12345678Aa" }))
            {
                using (Primary_db_contex db = new Primary_db_contex(_configuration))
                {
                    User user = db.users.Find(userId);
                    Group group = db.Groups.Find(groupId);
                    if (!await ftp.DirectoryExistsAsync(group.groupName+" id:"+group.Id.ToString()+"/d"+user.username))
                    {
                       await ftp.AutoConnectAsync();
                       bool a= await ftp.CreateDirectoryAsync("/"+group.groupName + "ID" + group.Id.ToString()+"/d"+user.username , true);
                       
                    }
                }
            }
        }

    }
    
}
/*
      public void CreateNewFtpDirectoryPathOnAdd(int groupId, int userId)
      {
          using (FtpClient ftp = new FtpClient("testftpservertaskbook.ucoz.net", new System.Net.NetworkCredential { UserName = "etestftpservertaskbook", Password = "12345678Aa" }))
          {
              using (Primary_db_contex db = new Primary_db_contex(_configuration))
              {
                  User user = db.users.Find(userId);
                  Group group = db.Groups.Find(groupId);
                  if (!UserFTPDirectories("Students/" + user.username).Contains(group.GroupName + "" + group.GroupEnv.Substring(0,3)))
                  {
                      ftp.Connect();
                      ftp.CreateDirectory("Students/" + user.username + "/" + 'd'+group.GroupName + "" + group.GroupEnv.Substring(0, 3), true);
                      GenerateAccessDatForGroupUserPair
                          (
                          group,
                          user,
                          "Students/" + user.username + "/" + group.GroupName + "" + group.GroupEnv.Substring(0, 3),
                          group.GroupName + "---" + group.GroupEnv
                          );

                  }

              }
          }
      }
      public void GenerateAccessDatForGroupUserPair(Group group, User user, string ftpPath, string repDir)
      {
          //using (FtpClient ftp = new FtpClient("testftpservertaskbook.ucoz.net", new System.Net.NetworkCredential { UserName = "etestftpservertaskbook", Password = "12345678Aa" }))
          //{
          var accessDatFileByteString = _accessFileGeneratorS.WriteAccessDatFile
               (
                   user.username,
                   group.GroupName,
                   group.GroupEnv,
                   group.GroupName,
                   "Students"+"/"+user.username+"/",
                   "testftpservertaskbook.ucoz.net",
                   "12345678Aa",
                   "etestftpservertaskbook",
                   ""

               );
          FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://testftpservertaskbook.ucoz.net"+"/"+ftpPath+"/"+"access.dat");
          request.Method = WebRequestMethods.Ftp.UploadFile;

          request.Credentials = new NetworkCredential("etestftpservertaskbook", "12345678Aa");
          //ftp.Connect();
          using (Stream requestStream = request.GetRequestStream())
          {
              requestStream.Write(accessDatFileByteString, 0, accessDatFileByteString.Length);
          }
          //}
      }
      */