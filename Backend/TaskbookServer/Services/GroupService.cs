using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentFTP;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TaskbookServer.Models;
using TaskbookServer.Models.TableObj;
namespace TaskbookServer.Services
{
    public class GroupService
    {
        private readonly IConfiguration _configuration;
        private readonly UserUtilityService _userUtilityService;
        public GroupService(IConfiguration configuration)
        {
            _configuration = configuration;
            _userUtilityService = new UserUtilityService(configuration);
        }
        public DataTable CreateGroupTableTeacher()
        {
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn { ColumnName = "GroupName", DataType = System.Type.GetType("System.String") });
            table.Columns.Add(new DataColumn { ColumnName = "Students", DataType = System.Type.GetType("System.String") });
            table.Columns.Add(new DataColumn { ColumnName = "GroupTasks", DataType = System.Type.GetType("System.String") });
            table.Columns.Add(new DataColumn { ColumnName = "LastUpdate", DataType = System.Type.GetType("System.DataTime") });

            return table;
        }
        public GroupTableRow CreateGroupTableRow(int groupid)
        {
            GroupTableRow result = new GroupTableRow();
            using (Primary_db_contex db = new Primary_db_contex(_configuration))
            {
                var a = db.Groups.Find(groupid);
                result.GroupId = groupid;
                result.GroupName = a.groupName;
                result.GropuDescribtion = a.groupDesc;
                result.Students = GetStudentsInGroupList(groupid);
                result.GroupTasks = GetGroupTaskList(groupid);
                result.LastUpdate = a.lastUpdateTime;

            }
            return result;

        }

        public List<StudyTask> GetUserGroupCompletedTasks(GroupStudent studentGroup)
        {
            using (Primary_db_contex db = new Primary_db_contex(_configuration))
            {
                List<StudyTask> result = new List<StudyTask> { };
                List<StudyTask> usercompletedTasks = _userUtilityService.GetUserCompletedTaskList(studentGroup.userId);
                foreach (var item in db.Group_Tasks.Where(x => x.groupId == studentGroup.groupId).Include(x => x.studyTask))
                {
                    if (usercompletedTasks.Any(x => x.Id == item.studyTaskId))
                    {
                        result.Add(item.studyTask);
                    }
                }
                return result.OrderBy(x => x.Id).ToList();

            }

        }
        public List<StudyTask> GetUserGroupInProgressTaskList(GroupStudent studentGroup)
        {
            using (Primary_db_contex db = new Primary_db_contex(_configuration))
            {
                List<StudyTask> result = new List<StudyTask> { };
                List<StudyTask> userinprogressTasks = _userUtilityService.GetUserInProgressTaskList(studentGroup.userId);
                foreach (var item in db.Group_Tasks.Where(x => x.groupId == studentGroup.groupId).Include(x => x.studyTask))
                {
                    if (userinprogressTasks.Any(x => x.Id == item.studyTaskId))
                    {
                        result.Add(item.studyTask);
                    }
                }
                return result.OrderBy(x => x.Id).ToList();
            }


        }
        public List<StudyTask> GetGroupTaskList(int groupid)
        {
            using (Primary_db_contex db = new Primary_db_contex(_configuration))
            {
                var a = db.Group_Tasks.Where(x => x.groupId == groupid && x.active == true).Include(x => x.studyTask).ToList();
                List<StudyTask> b = new List<StudyTask>();
                foreach (var item in a)
                {
                    b.Add(item.studyTask);
                }
                return b.OrderBy(x => x.Id).ToList();
            }

        }
        public List<User> GetStudentsInGroupList(int groupid)
        {

            using (Primary_db_contex db = new Primary_db_contex(_configuration))
            {
                var a = db.Student_Groups.Where(x => x.groupId == groupid && x.inGroupStatus == true).Include(x => x.user).ToList();
                List<User> b = new List<User>();
                foreach (var item in a)
                {
                    b.Add(item.user);
                }
                return b;
            }

        }


        public List<GroupResultTable> SortResultTable(List<GroupResultTable> unsortedList, string SortCollum, string sortBy)
        {
            switch (SortCollum)
            {
                case "FIO":
                    {
                        if (sortBy == "ASC")
                        {
                            return unsortedList.OrderBy(x => x.user.lastName).ToList();
                        }
                        else
                            return unsortedList.OrderByDescending(x => x.user.lastName).ToList();


                    }
                case "CompletedTasks":
                    {
                        if (sortBy == "ASC")
                        {
                            return unsortedList.OrderBy(x => x.CompletedTasks).ToList();
                        }
                        else
                            return unsortedList.OrderByDescending(x => x.CompletedTasks).ToList();
                    }
                case "Default":
                    {
                        if (sortBy == "ASC")
                        {
                            return unsortedList.OrderBy(x => x.user.lastName).ToList();
                        }
                        else
                            return unsortedList.OrderByDescending(x => x.user.lastName).ToList();
                    }
                case "InProcessTasks":
                    {
                        if (sortBy == "ASC")
                        {
                            return unsortedList.OrderBy(x => x.InprogressTasks).ToList();
                        }
                        else
                            return unsortedList.OrderByDescending(x => x.InprogressTasks).ToList();
                    }
                case "Rating":
                    {
                        if (sortBy == "ASC")
                        {
                            return unsortedList.OrderBy(x => x.Raiting).ToList();
                        }
                        else
                            return unsortedList.OrderByDescending(x => x.Raiting).ToList();

                    }
                case "LastVisit":
                    {
                        if (sortBy == "ASC")
                        {
                            return unsortedList.OrderBy(x => x.LastVistit).ToList();
                        }
                        else
                            return unsortedList.OrderByDescending(x => x.LastVistit).ToList();
                    }

            }
            return unsortedList;

        }

        public async Task GenerateInvivtationTokenOnDb(int groupId, int userId)
        {
            string Invatetiontoken = GenerateInvitaionToken();
            try
            {
                using (Primary_db_contex db = new Primary_db_contex(_configuration))
                {

                    db.invitation_to_groups.Add(new InvitationToGroup
                    {
                        groupId = groupId,
                        creationTime = DateTime.Now,
                        invitationToken = Invatetiontoken,
                        userCreatedId = userId,
                        invitationStatus = "Active",

                        maxPeopleCount = 100,
                        currentAcceptedPeople = 0
                    });
                    await db.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {

            }
        }

        public string GenerateInvitaionToken()
        {

            var rnd = new Random();
            var s = new StringBuilder();

            for (int i = 0; i < 30; i++)
            {

                s.Append((char)rnd.Next('a', 'z'));
                s.Append((char)rnd.Next('A', 'Z'));
            }


            return s.ToString();

        }


        public Group GetGroupObjectFromGroupId(int id) // Функция, которая возвращает объект класса group по ее уникальному полю GroupCred
        {
            using (Primary_db_contex db = new Primary_db_contex(_configuration))
            {
                return db.Groups.Include(x => x.teacher).Where(x => x.Id == id).FirstOrDefault();
            }
        }


        public async Task GenerateGroupFtpDirectory(string path, int groupId)
        {
            try
            {
                using (FtpClient ftp = new FtpClient(_configuration["FtpData:ftpHost"],
                    new System.Net.NetworkCredential { UserName = _configuration["FtpData:ftpLogin"], 
                        Password = _configuration["FtpData:ftpPassword"] }))
                {
                    await ftp.AutoConnectAsync();
                    if (!await ftp.DirectoryExistsAsync(path))
                    {

                        bool a = await ftp.CreateDirectoryAsync(path, true);

                    }
                    using (Primary_db_contex db = new Primary_db_contex(_configuration))
                    {
                        var a = await db.Groups.FindAsync(groupId);
                        a.groupFtpFullPath = path;
                        db.Update(a);
                        await db.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public async Task GenerateStudentFolderInGroupFTP(int groupId, User student)
        {
            try
            {
                using (Primary_db_contex db = new Primary_db_contex(_configuration))
                {
                    Group group = await db.Groups.FindAsync(groupId);


                    using (FtpClient ftp = new FtpClient(_configuration["FtpData:ftpHost"],
                        new System.Net.NetworkCredential
                        {
                            UserName = _configuration["FtpData:ftpLogin"],
                            Password = _configuration["FtpData:ftpPassword"]
                        }))
                    {

                        await ftp.AutoConnectAsync();
                        if (!await ftp.DirectoryExistsAsync(group.groupFtpFullPath+'/'+student.username))
                        {

                            bool a = await ftp.CreateDirectoryAsync(group.groupFtpFullPath + '/' + student.username, true);

                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }






    }
}
