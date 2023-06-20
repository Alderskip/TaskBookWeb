using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TaskbookServer.Models;
using TaskbookServer.Models.TableObj;
using TaskbookServer.Services;
using static TaskbookServer.Services.ParserForTaskGroupsService;

namespace TaskbookServer.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ResultService _resultService;
        private readonly UserUtilityService _userUtilityService;
        private readonly RoleService _roleService;
        private readonly TimeService _timeService;
        private readonly GroupService _groupService;
        private readonly EmailService _emailService;
        private readonly FtpTaskResultService _ftpService;
        private readonly AccessFileGeneratorS _accessService;
        private readonly ParserForTaskGroupsService _parserForTaskGroupsService;
        public GroupController(IConfiguration configuration)
        {
            _configuration = configuration;
            _resultService = new ResultService(configuration);
            _userUtilityService = new UserUtilityService(configuration);
            _roleService = new RoleService(configuration);
            _timeService = new TimeService(configuration);
            _groupService = new GroupService(configuration);
            _emailService = new EmailService(configuration);
            _ftpService = new FtpTaskResultService(configuration);
            _accessService = new AccessFileGeneratorS(configuration);
            _parserForTaskGroupsService = new ParserForTaskGroupsService(configuration);
        }

        //Новые api
        [AllowAnonymous]
        [HttpGet("TestParser")]
        public async Task<IActionResult> TestParser()
        {
            _parserForTaskGroupsService.test();
            return Ok();

        }
        [HttpGet("GetGroupCount")]
        public async Task<IActionResult> GetGroupCount()
        {
            var t = Request.Headers["Authorization"].ToString();
            try
            {
                using (Primary_db_contex db = new Primary_db_contex(_configuration))
                {
                    var response = await db.Student_Groups.Where(x => x.userId == _userUtilityService.GetUserObjectFromToken(t).Id).CountAsync();
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }
        [HttpGet]


        [HttpGet("GetGroupTasks")]
        public async Task<IActionResult> GetGroupTasks([FromQuery] int groupId)
        {
            try
            {
                using (Primary_db_contex db = new Primary_db_contex(_configuration))
                {
                    return Ok(await db.Group_Tasks.Include(x => x.studyTask).Where(x => x.groupId == groupId).ToListAsync());
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }


        }


        [HttpGet("GetGroupTaskCount")]
        public async Task<IActionResult> GetCourseTasks([FromQuery] int groupId)
        {
            using (Primary_db_contex db = new Primary_db_contex(_configuration))
            {
                return Ok(await db.Group_Tasks.Where(x => x.groupId == groupId).CountAsync());
            }
        }


        [HttpGet("GetGroupsTeacher1")]
        public async Task<IActionResult> GetGroupsTeacher([FromQuery] int currentPage)
        {
            var t = Request.Headers["Authorization"].ToString();
            if (!_roleService.CheckTeacher(t))
            {
                return Unauthorized();
            }
            using (Primary_db_contex db = new Primary_db_contex(_configuration))
            {
                List<GroupTableRowStudent> res = new List<GroupTableRowStudent>();
                foreach (var elem in db.Groups.Where(x => x.teacherId == _userUtilityService.GetUserObjectFromToken(t).Id)
                    .Skip(currentPage * 5 - 5).Take(5))
                {
                    res.Add(new GroupTableRowStudent
                    {
                        Id = elem.Id,
                        // teacherFIO = elem.Group.Teacher.lastName + " " + elem.Group.Teacher.firstName + " " + elem.Group.Teacher.secondOrFathersName,
                        enviroment = elem.groupEnv,
                        groupDesc = elem.groupDesc,
                        groupName = elem.groupName,
                        taskToDo = true
                    });
                }
                return Ok(res);
            }
        }



        [HttpGet("GetGroupCountTeacher")]
        public async Task<IActionResult> GetGroupCountTeacher()
        {
            var t = Request.Headers["Authorization"].ToString();
            if (!_roleService.CheckTeacher(t))
            {
                return Unauthorized();
            }
            using (Primary_db_contex db = new Primary_db_contex(_configuration))
            {
                var response = await db.Groups.Where(x => x.teacherId == _userUtilityService.GetUserObjectFromToken(t).Id).CountAsync();
                return Ok(response);
            }
        }



        [HttpGet("GetGroupStudentsTeacher")]
        public async Task<IActionResult> GetGroupStudentsTeacher([FromQuery] int groupId)
        {
            var t = Request.Headers["Authorization"].ToString();
            if (!_roleService.CheckTeacher(t))
            {
                return Unauthorized();
            }
            List<User> res = new List<User>();
            using (Primary_db_contex db = new Primary_db_contex(_configuration))
            {
                foreach (var elem in db.Student_Groups.Include(x => x.group.teacher).Include(x => x.user).Where(x => x.group.teacherId == _userUtilityService.GetUserObjectFromToken(t).Id && x.groupId == groupId && x.inGroupStatus==true))
                {
                    res.Add(new User
                    {
                        Id=elem.userId,
                        username = elem.user.username,
                        firstName = elem.user.firstName,
                        lastName = elem.user.lastName,
                        secondOrFathersName = elem.user.secondOrFathersName
                    });
                }
                return Ok(res);
            }
        }



        [HttpPost("CreateGroupTeacher")]
        public async Task<IActionResult> CreateGroupTeacher([FromBody] Group group)
        {
            var t = Request.Headers["Authorization"].ToString();

            if (!_roleService.CheckTeacher(t))
            {
                return Unauthorized();
            }
            var teacherId = _userUtilityService.GetUserObjectFromToken(t).Id;
            group.teacherId = teacherId;
            group.lastUpdateTime = DateTime.Now;
            try
            {
                using (Primary_db_contex db = new Primary_db_contex(_configuration))
                {

                    var b = await db.Groups.AddAsync(group);

                    await db.SaveChangesAsync();
                    await _groupService.GenerateGroupFtpDirectory(b.Entity.groupName + "ID" + b.Entity.Id.ToString(), b.Entity.Id);
                    await _groupService.GenerateInvivtationTokenOnDb(b.Entity.Id, b.Entity.teacherId);
                    return Ok();
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }



        [HttpPost("AddTaskToGroupTeacher")]
        public async Task<IActionResult> AddTaskToGroupTeacher([FromBody] GroupTask task)
        {
            var t = Request.Headers["Authorization"].ToString();

            if (!_roleService.CheckTeacher(t))
            {
                return Unauthorized();
            }
            try
            {
                using (Primary_db_contex db = new Primary_db_contex(_configuration))
                {
                    Group group = db.Groups.Find(task.groupId);
                    StudyTask studyTask = db.study_tasks.Find(task.studyTaskId);
                    if (!await db.Group_Tasks.AnyAsync(x => x.studyTaskId == studyTask.Id && x.groupId == group.Id))
                    {
                        task.active = true;
                        if (group.gradeSys == "credit")
                        {
                            task.maxTaskPointValueInGroup = 1;
                            await db.Group_Tasks.AddAsync(task);
                        }
                        else if (group.gradeSys == "default")
                        {
                            task.maxTaskPointValueInGroup = task.studyTask.taskPointValue;
                            await db.Group_Tasks.AddAsync(task);
                        }
                        else
                        {
                            if (task.maxTaskPointValueInGroup <= 0)
                                task.maxTaskPointValueInGroup = studyTask.taskPointValue;
                            await db.Group_Tasks.AddAsync(task);

                        }

                    }
                    else
                    {
                        var a = db.Group_Tasks.Where(x => x.studyTaskId == studyTask.Id && x.groupId == group.Id).FirstOrDefault();
                        db.Group_Tasks.Where(x => x.studyTaskId == studyTask.Id && x.groupId == group.Id).FirstOrDefault().active = !a.active;
                    }

                    await db.SaveChangesAsync();
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }



        [HttpGet("GetGroupResTeacher")]
        public async Task<IActionResult> GetGroupResTeacher([FromQuery] int groupId)
        {
            var t = Request.Headers["Authorization"].ToString();
            if (!_roleService.CheckTeacher(t))
            {
                return Unauthorized();
            }
            try
            {
                using (Primary_db_contex db = new Primary_db_contex(_configuration))
                {
                    return Ok(await db.groupGrades.Include(x => x.student).Include(x => x.groupTask.studyTask).Where(x => x.groupId == groupId).ToListAsync());
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("GetGroupTeacher")]
        public async Task<IActionResult> GetGroupTeacher([FromQuery] int groupId)
        {
            var t = Request.Headers["Authorization"].ToString();
            if (!_roleService.CheckTeacher(t))
            {
                return Unauthorized();
            }
            try
            {
                using (Primary_db_contex db = new Primary_db_contex(_configuration))
                {

                    return Ok(await db.Groups.Where(x => x.Id == groupId).FirstOrDefaultAsync());
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpGet("GetGroupsTeacher")]
        public async Task<IActionResult> GetGroupsTeacher()
        {
            var t = Request.Headers["Authorization"].ToString();
            if (!_roleService.CheckTeacher(t))
            {
                return Unauthorized();
            }
            try
            {
                using (Primary_db_contex db = new Primary_db_contex(_configuration))
                {
                    var a = _userUtilityService.GetUserObjectFromToken(t).Id;
                    return Ok(await db.Groups.Where(x => x.teacherId == a).OrderBy(x => x.lastUpdateTime).ToListAsync());
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("GetGroupInvitationTokenTeacher")]
        public async Task<IActionResult> GetGroupInvitationTokenTeacher([FromQuery] int groupId)
        {
            var t = Request.Headers["Authorization"].ToString();
            if (!_roleService.CheckTeacher(t))
            {
                return Unauthorized();
            }
            try
            {
                using (Primary_db_contex db = new Primary_db_contex(_configuration))
                {
                    return Ok(db.invitation_to_groups.Where(x => x.groupId == groupId && x.invitationStatus == "Active").FirstOrDefault());
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpPost("AddStudentToGroupByUsernameTeacher")]
        public async Task<IActionResult> AddStudentToGroupByUsernameTeacher([FromBody] GroupStudent gr)
        {
            var t = Request.Headers["Authorization"].ToString();
            if (!_roleService.CheckTeacher(t))
            {
                return Unauthorized();
            }
            try
            {
                using (Primary_db_contex db = new Primary_db_contex(_configuration))
                {
                    User user = _userUtilityService.GetUserFromUserUsername(gr.user.username);
                    if (user.role != "Student")
                        return BadRequest();
                    if (await db.Student_Groups.AnyAsync(x => x.userId == user.Id && x.groupId == gr.groupId))
                    {
                        var a = db.Student_Groups.Where(x => x.userId == user.Id && x.groupId == gr.groupId).FirstOrDefault();
                        a.inGroupStatus = true;
                        db.Student_Groups.Update(a);
                    }
                    else
                    {
                        await db.Student_Groups.AddAsync(new GroupStudent
                        {
                            groupId = gr.groupId,
                            userId = user.Id,
                            gradeInGroup = 0,
                            inGroupStatus = true
                        });
                    }
                   
                    await db.SaveChangesAsync();
                    //   await _groupService.GenerateStudentFolderInGroupFTP(gr.groupId, user);
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpPost("AddByTokenS")]
        public async Task<IActionResult> AddStudentToGroupByInvitationTokenS([FromQuery] string token)
        {
            var t = Request.Headers["Authorization"].ToString();
            var user = _userUtilityService.GetUserObjectFromToken(t);
            if (user.role == "Teacher")
            {
                return BadRequest();
            }
            try
            {
                using (Primary_db_contex db = new Primary_db_contex(_configuration))
                {
                    var invitation = await db.invitation_to_groups.Where(x => x.invitationToken == token && x.invitationStatus == "Active").FirstOrDefaultAsync();
                    await db.recieved_Invitations.AddAsync(new RecievedInvitations
                    {
                        invitation = invitation,
                        invitationId = invitation.Id,
                        userRecievedId = user.Id,
                        reactionTime = DateTime.Now,
                        recievedInvitationStatus = "Succesful"
                    });
                    await db.Student_Groups.AddAsync(new GroupStudent { groupId = invitation.groupId, userId = user.Id, inGroupStatus = true, gradeInGroup = 0 });
                    await db.SaveChangesAsync();
                    // await _groupService.GenerateStudentFolderInGroupFTP(invitation.groupId, user);
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        [HttpGet("GetStudyTaskGroups")]
        public async Task<IActionResult> GetStudyTaskGroups()
        {
            var t = Request.Headers["Authorization"].ToString();
            if (!_roleService.CheckTeacher(t))
            {
                return Unauthorized();
            }
            try
            {
                using (Primary_db_contex db = new Primary_db_contex(_configuration))
                {
                    return Ok((await db.studyTaskGroups.ToListAsync()));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet("GetGroupTasksAddingModel")]
        public async Task<IActionResult> GetGroupTasksAddingModel(int studyTaskGroupId, int groupId)
        {
            var t = Request.Headers["Authorization"].ToString();
            if (!_roleService.CheckTeacher(t))
            {
                return Unauthorized();
            }
            try
            {
                List<GroupTask> res = new List<GroupTask>();
                using (Primary_db_contex db = new Primary_db_contex(_configuration))
                {
                    Group group = await db.Groups.FindAsync(groupId);
                    res.AddRange(db.Group_Tasks.Include(x => x.studyTask).Include(x => x.group).Where(x => x.groupId == groupId && x.studyTask.studyTaskGroupId == studyTaskGroupId));
                    foreach (var item in db.study_tasks.Where(x => x.studyTaskGroupId == studyTaskGroupId))
                    {
                        if (!res.Exists(x => x.studyTask == item))
                        {

                            res.Add(new GroupTask
                            {
                                groupId = groupId,
                                active = false,
                                studyTaskId = item.Id,
                                studyTask = item,
                                isAutoGraded = false,
                                isMandatory = true,
                                maxTaskPointValueInGroup = group.gradeSys == "credit" ? 1 : item.taskPointValue
                            });
                        }
                    }
                    return Ok(res.OrderBy(res => res.studyTaskId).ToList());
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("ChangeGroupTaskParams")]
        public async Task<IActionResult> ChangeGroupTaskParams([FromBody] GroupTask task)
        {
            var t = Request.Headers["Authorization"].ToString();
            if (!_roleService.CheckTeacher(t))
            {
                return Unauthorized();
            }
            try
            {
                using (Primary_db_contex db = new Primary_db_contex(_configuration))
                {
                    var taskToChange = await db.Group_Tasks.FindAsync(task.Id);
                    if (taskToChange != null)
                    {
                        taskToChange.isMandatory = task.isMandatory;
                        taskToChange.isAutoGraded = task.isAutoGraded;
                        taskToChange.maxTaskPointValueInGroup = task.maxTaskPointValueInGroup;
                    }
                    db.Group_Tasks.Update(taskToChange);
                    await db.SaveChangesAsync();
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        [HttpPost("UpdateGroupResults")]
        public async Task<IActionResult> UpdateGroupRes([FromBody] Group group)
        {
            var t = Request.Headers["Authorization"].ToString();
            if (!_roleService.CheckTeacher(t))
            {
                return Unauthorized();
            }
            try
            {
                await _ftpService.CheckAndUpdateGroupResFTP(group.Id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("GradeTaskTeacher")]
        public async Task<IActionResult> GradeTaskTeacher([FromBody] GroupResult result)
        {
            var t = Request.Headers["Authorization"].ToString();
            if (!_roleService.CheckTeacher(t))
            {
                return Unauthorized();
            }
            try
            {
                using (Primary_db_contex db = new Primary_db_contex(_configuration))
                {
                    var resultToGrade = await db.groupGrades.FindAsync(result.Id);
                    if (result.resType == "Задание выполнено")
                    {
                        resultToGrade.IsGraded = true;
                        resultToGrade.pointGradeValue = result.pointGradeValue;
                        var studentGradeToUpdate = await db.Student_Groups.Where(x => x.userId == result.studentId && x.groupId == result.groupId).FirstOrDefaultAsync();
                        studentGradeToUpdate.gradeInGroup += resultToGrade.pointGradeValue;
                        db.Student_Groups.Update(studentGradeToUpdate);
                    }
                    resultToGrade.seen = true;
                    db.groupGrades.Update(resultToGrade);
                    
                    await db.SaveChangesAsync();
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [AllowAnonymous]
        [HttpGet("DowlandAccessDatStudent")]
        public async Task<IActionResult> DowlandAccessDatStudent([FromQuery] int groupId)
        {
            var t = Request.Headers["Authorization"].ToString();
            if (_roleService.CheckTeacher(t))
            {
                return Unauthorized();
            }
            try
            {
                using (var db = new Primary_db_contex(_configuration))
                {
                    User user = _userUtilityService.GetUserObjectFromToken(t);
                    Group group = await db.Groups.FindAsync(groupId);
                    byte[] res = _accessService.WriteAccessDatFile(
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

                    Stream stream = new MemoryStream(res);
                    return File(stream, "application/octet-stream", "access.dat");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        [HttpGet("GetStudentResults")]
        public async Task<IActionResult> GetStudentRes([FromQuery] int userId, int groupId)
        {
            var t = Request.Headers["Authorization"].ToString();
            if (_roleService.CheckTeacher(t))
            {
                return Unauthorized();
            }
            try
            {
                using (var db = new Primary_db_contex(_configuration))
                {
                    return Ok(await db.groupGrades.Include(x => x.groupTask.studyTask).Where(x => x.studentId == userId && x.groupId == groupId).ToListAsync());
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }


        }
        [HttpPut("RemoveFromGroupTeacher")]
        public async Task<IActionResult> RemoveFromGroupTeacher([FromBody] GroupStudent groupStudent)
        {
            var t = Request.Headers["Authorization"].ToString();
            if (!_roleService.CheckTeacher(t))
            {
                return Unauthorized();
            }
            try
            {
                using (var db = new Primary_db_contex(_configuration))
                {
                    var studentToRemove = await db.Student_Groups.Where(x => x.userId == groupStudent.userId && x.groupId == groupStudent.groupId).FirstOrDefaultAsync();
                    studentToRemove.inGroupStatus = false;
                    db.Student_Groups.Update(studentToRemove);
                    await db.SaveChangesAsync();
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("GetGroupsStudent")]
        public async Task<IActionResult> GetGroupsStudent()
        {
            var t = Request.Headers["Authorization"].ToString();
            User user = _userUtilityService.GetUserObjectFromToken(t);
            if (user.role=="Teacher")
            {
                return Unauthorized();
            }

            try
            {
                using (var db = new Primary_db_contex(_configuration))
                {
                    List<Group> res = new List<Group>();
                    foreach (var item in  db.Student_Groups.Include(x=>x.group).Where(x => x.userId == user.Id && x.inGroupStatus == true))
                    {
                        res.Add(item.group);
                    }
                    return Ok(res);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet("DowlandInitialResultFile")]
        public async Task<IActionResult> DowlandInitialResultFile([FromQuery] int groupId)
        {
            var t = Request.Headers["Authorization"].ToString();
            if (_roleService.CheckTeacher(t))
            {
                return Unauthorized();
            }
            try
            {
                using (var db = new Primary_db_contex(_configuration))
                {
                    User user = _userUtilityService.GetUserObjectFromToken(t);
                    Group group = await db.Groups.FindAsync(groupId);
                    byte[] res = _accessService.WriteInitialResultFile(user.username, group.groupEnv, "C:\\Program Files (x86)\\PT4");
                    Stream stream = new MemoryStream(res);
                    return File(stream, "application/octet-stream", "results.dat");
                    
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }



        /* Ненужный api? 
        [HttpGet("GetStudentUnGradedTasks")]
        public async Task<IActionResult> GetStudentTasks([FromQuery] int groupId)
        {
            var t = Request.Headers["Authorization"].ToString();
            if (_roleService.CheckTeacher(t))
            {
                return Unauthorized();
            }
            try
            {
                using (var db = new Primary_db_contex(_configuration))
                {
                    return Ok(await db.Group_Tasks.Where(x => x.groupId == groupId).ToListAsync());
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }


        }
        */

        /*Cтарые api
         * 
         * 
        [HttpPost("GenerateGroupInvitationTokenT")]
        public IActionResult GenerateGroupInvitationToken([FromQuery] int groupid)
        {
            var t = Request.Headers["Authorization"].ToString();
            User user = _userUtilityService.GetUserObjectFromToken(t);
            if (user.role != "Teacher" && user.role != "Admin")
            {
                return Unauthorized();
            }
            string Invatetiontoken = _groupService.GenerateInvitaionToken();
            using (Primary_db_contex db = new Primary_db_contex(_configuration))
            {

                db.invitation_to_groups.Add(new InvitationToGroup
                {
                    groupId = groupid,
                    creationTime = DateTime.Now,
                    invitationToken = Invatetiontoken,
                    userCreatedId = user.Id,
                    invitationStatus = "Active",
                    InvitationType = "GroupInvitation",
                    maxPeopleCount = 100,
                    currentAcceptedPeople = 0
                });
                db.SaveChanges();

            }
            return Ok(Invatetiontoken);
        }


        [HttpGet("GetGroupsTableTeacher")]
        public IActionResult GetGroupsTableTeacher([FromQuery] PaginationFilter filter)
        {
            //DataTable GroupTable = _groupService.CreateGroupTableTeacher();
            var t = Request.Headers["Authorization"].ToString();
            _timeService.LastVisitUpdate(t);
            string role = _roleService.GetUserRole(t);
            if (role != "Teacher" && role != "Admin")
                return Unauthorized();
            User user = _userUtilityService.GetUserObjectFromToken(t);
            List<GroupTableRow> result = new List<GroupTableRow>();
            using (Primary_db_contex db = new Primary_db_contex(_configuration))
            {
                var a = db.Groups.Where(x => x.teacher == user).ToList();
                foreach (var item in a)
                {
                    result.Add(_groupService.CreateGroupTableRow(item.Id));
                }
                JsonResult jsonresult = new JsonResult(result);
                return Ok(jsonresult);


            }

        }
        [HttpGet("GetGroupCountold")]
        public IActionResult GetGroupCountold()
        {
            var t = Request.Headers["Authorization"].ToString();
            _timeService.LastVisitUpdate(t);
            User user = _userUtilityService.GetUserObjectFromToken(t);
            using (Primary_db_contex db = new Primary_db_contex(_configuration))
            {
                return Ok(db.Student_Groups.Where(x => x.user.Id == user.Id && x.inGroupStatus == true).Count());
            }
        }
        [HttpGet("GetUserGroupTable")]
        public IActionResult GetUserTable([FromQuery] PaginationFilter filter)
        {
            var t = Request.Headers["Authorization"].ToString();
            _timeService.LastVisitUpdate(t);
            string role = _roleService.GetUserRole(t);
            if (role != "Teacher" && role != "Admin")
                return Unauthorized();
            List<UserTableRow> result = new List<UserTableRow> { };
            List<User> UsersInGroup = _groupService.GetStudentsInGroupList(filter.groupid);
            using (Primary_db_contex db = new Primary_db_contex(_configuration))
            {

                foreach (var item in db.users.Where(x => x.role == "Student" || x.role == "Student\n"))
                {


                    if (UsersInGroup.Any(x => x.Id == item.Id))
                    {
                        result.Add(new UserTableRow { User = item, InGroup = true });

                    }
                    else
                    {
                        result.Add(new UserTableRow { User = item, InGroup = false });
                    }

                    //result = _groupService.SortAndFindStringInUserTable(result, filter);


                }
                //result = result.Skip(filter.PageNumber).Take(filter.PageSize).ToList();
                return Ok(result);
            }

        }

        [HttpGet("GetGroupResultsTable")]
        public IActionResult GetGroupResultsTable([FromQuery] PaginationFilter filter)
        {
            var t = Request.Headers["Authorization"].ToString();
            _timeService.LastVisitUpdate(t);
            string role = _roleService.GetUserRole(t);
            if (role != "Teacher" && role != "Admin")
                return Unauthorized();
            List<GroupResultTable> result = new List<GroupResultTable> { };
            using (Primary_db_contex db = new Primary_db_contex(_configuration))
            {
                foreach (var item in db.Student_Groups.Where(x => x.groupId == filter.groupid && x.inGroupStatus == true).Include(x => x.user))
                {

                    //result.Add(_groupService.CreateGroupResultTableRow(item));


                }
            }
            result = _groupService.SortResultTable(result, filter.SortСolumn, filter.SortBy);
            result = result.Skip(filter.PageNumber).Take(filter.PageSize).ToList();
            JsonResult jsonresult = new JsonResult(result);
            return Ok(jsonresult);
        }



        [HttpGet("GetStudyTaskTable")]
        public IActionResult GetStudyTaskTable([FromQuery] PaginationFilter filter)
        {
            var t = Request.Headers["Authorization"].ToString();
            _timeService.LastVisitUpdate(t);
            string role = _roleService.GetUserRole(t);
            if (role != "Teacher" && role != "Admin")
                return Unauthorized();
            using (Primary_db_contex db = new Primary_db_contex(_configuration))
            {
                var result = db.study_tasks.Where(x => x.Id - 160 >= filter.PageNumber && x.Id - 160 < filter.PageNumber + filter.PageSize).OrderBy(x => x.Id).ToList();
                return Ok(new JsonResult(result));

            }

        }
        [HttpGet("GetGroupStudyTaskTable")]
        public IActionResult GetGroupStudyTaskTable([FromQuery] PaginationFilter filter)
        {
            var t = Request.Headers["Authorization"].ToString();
            _timeService.LastVisitUpdate(t);
            string role = _roleService.GetUserRole(t);
            if (role != "Teacher" && role != "Admin")
                return Unauthorized();
            List<StudyTaskTableRow> result1 = new List<StudyTaskTableRow> { };
            using (Primary_db_contex db = new Primary_db_contex(_configuration))
            {
                var b = db.Group_Tasks.Where(x => x.groupId == filter.groupid && x.active == true).Include(x => x.studyTask).ToList();
                var result = db.study_tasks.Where(x => x.Id - 160 >= filter.PageNumber && x.Id - 160 < filter.PageNumber + filter.PageSize).ToList();
                foreach (var item in result)
                {
                    result1.Add(new StudyTaskTableRow { StudyTask = item, Active = false });
                }
                foreach (var item in b)
                {

                    if (result.Contains(item.studyTask))
                    {
                        result1.Where(x => x.StudyTask == item.studyTask).FirstOrDefault().Active = true;
                    }
                }
                return Ok(new JsonResult(result1));

            }

        }
        [HttpPost("CreateAndSendInvitationToGroup")]
        public IActionResult CreateAndSendInvitationToGroup([FromBody] string email, [FromQuery] int groupid)
        {
            var t = Request.Headers["Authorization"].ToString();
            _timeService.LastVisitUpdate(t);
            User user = _userUtilityService.GetUserObjectFromToken(t);
            if (user.role != "Teacher" && user.role != "Admin")
            {
                return Unauthorized();
            }
            string invatetiontoken = _groupService.GenerateInvitaionToken();
            using (Primary_db_contex db = new Primary_db_contex(_configuration))
            {
                db.invitation_to_groups.Add(new InvitationToGroup
                {
                    UserCreated = user,
                    groupId = groupid,
                    CreationTime = DateTime.Now,
                    InvitationToken = invatetiontoken,
                    UserCreatedId = user.Id,
                    InvitationStatus = "Active",



                }); ;
                db.SaveChanges();
                EmailService.SendEmail(email, invatetiontoken).Wait();


            }
            return Ok();
        }
        [HttpPost("GenerateGroupInvitationTokenT")]
        public IActionResult GenerateGroupInvitationToken([FromQuery] int groupid)
        {
            var t = Request.Headers["Authorization"].ToString();
            _timeService.LastVisitUpdate(t);
            User user = _userUtilityService.GetUserObjectFromToken(t);
            if (user.role != "Teacher" && user.role != "Admin")
            {
                return Unauthorized();
            }
            string Invatetiontoken = _groupService.GenerateInvitaionToken();
            using (Primary_db_contex db = new Primary_db_contex(_configuration))
            {

                db.invitation_to_groups.Add(new InvitationToGroup
                {
                    groupId = groupid,
                    CreationTime = DateTime.Now,
                    InvitationToken = Invatetiontoken,
                    UserCreatedId = user.Id,
                    InvitationStatus = "Active",
                    InvitationType = "GroupInvitation",
                    MaxPeopleCount = 100,
                    CurrentAcceptedPeople = 0
                });
                db.SaveChanges();

            }
            return Ok(Invatetiontoken);
        }
        [HttpGet("ShowGroupInvitationToken")]
        public IActionResult ShowGroupInvitationToken([FromQuery] int groupid)
        {
            var t = Request.Headers["Authorization"].ToString();
            _timeService.LastVisitUpdate(t);
            User user = _userUtilityService.GetUserObjectFromToken(t);
            if (user.role != "Teacher" && user.role != "Admin")
            {
                return Unauthorized();
            }
            using (Primary_db_contex db = new Primary_db_contex(_configuration))
            {
                try
                {
                    return Ok(db.invitation_to_groups.Where(x => x.groupId == groupid).LastOrDefault());
                }
                catch (Exception ex)
                {
                    return BadRequest("Кода приглашения не существет, возможно вы его не сгенерировали");
                }



            }

        }
        [HttpGet("ShowGroupInvitationTokenString")]
        public IActionResult ShowGroupInvitationTokenString([FromQuery] int groupid)
        {
            var t = Request.Headers["Authorization"].ToString();
            _timeService.LastVisitUpdate(t);
            User user = _userUtilityService.GetUserObjectFromToken(t);
            if (user.role != "Teacher" && user.role != "Admin")
            {
                return Unauthorized();
            }
            using (Primary_db_contex db = new Primary_db_contex(_configuration))
            {
                try
                {
                    return Ok(db.invitation_to_groups.Where(x => x.groupId == groupid).LastOrDefault().InvitationToken);
                }
                catch (Exception ex)
                {
                    return BadRequest("Кода приглашения не существет, возможно вы его не сгенерировали");
                }



            }

        }
        [HttpGet("GetInvitationToGroupInfoUsers")]
        public IActionResult GetInvitationToGroupInfoUsers([FromQuery] PaginationFilter filter)
        {
            var t = Request.Headers["Authorization"].ToString();
            _timeService.LastVisitUpdate(t);
            User user = _userUtilityService.GetUserObjectFromToken(t);
            if (user.role != "Teacher" && user.role != "Admin")
            {
                return Unauthorized();
            }
            using (Primary_db_contex db = new Primary_db_contex(_configuration))
            {
                return Ok(db.recieved_Invitations.Where(x => x.Invitation.groupId == filter.groupid).Include(x => x.Invitation).ToList());

            }

        }
        [HttpPost("GenerateInvitationTokenTeacher")]
        public IActionResult GenerateInvitationTokenTeacher([FromQuery] int groupid)
        {
            var t = Request.Headers["Authorization"].ToString();
            _timeService.LastVisitUpdate(t);
            User user = _userUtilityService.GetUserObjectFromToken(t);
            if (user.role != "Teacher" && user.role != "Admin")
            {
                return Unauthorized();
            }
            string Invatetiontoken = _groupService.GenerateInvitaionToken();
            using (Primary_db_contex db = new Primary_db_contex(_configuration))
            {
                if (db.invitation_to_groups.Any(x => x.groupId == groupid))
                {
                    db.invitation_to_groups.Where(x => x.groupId == groupid).FirstOrDefault().InvitationStatus = "Inactive";
                }
                db.invitation_to_groups.Add(new InvitationToGroup
                {
                    groupId = groupid,
                    CreationTime = DateTime.Now,
                    InvitationToken = Invatetiontoken,
                    UserCreatedId = user.Id,
                    InvitationStatus = "Active",
                    InvitationType = "Personal",
                    MaxPeopleCount = 1,
                    CurrentAcceptedPeople = 0

                }); ;
                db.SaveChanges();

            }
            return Ok(Invatetiontoken);
        }
        [HttpPost("AddStudentToGroupByInvitationToken")]
        public IActionResult AddStudentToGroupByInvitationToken([FromQuery] string Invitationtoken)
        {
            var t = Request.Headers["Authorization"].ToString();
            _timeService.LastVisitUpdate(t);
            ;
            User user = _userUtilityService.GetUserObjectFromToken(t);
            using (Primary_db_contex db = new Primary_db_contex(_configuration))
            {
                try
                {
                    var a = db.invitation_to_groups.Where(x => x.InvitationToken == Invitationtoken).Include(x => x.group).Include(x => x.group.teacher).FirstOrDefault();
                    if (a.InvitationStatus == "Active" && a.MaxPeopleCount != a.CurrentAcceptedPeople)
                    {
                        db.recieved_Invitations.Add(new RecievedInvitations
                        {
                            InvitationId = a.Id,
                            UserRecievedId = user.Id, 
                            ReactionTime = DateTime.Now,
                            RecievedInvitationStatus = "Accepted"
                        });
                        if (db.Student_Groups.Any(x => x.groupId == a.group.Id && x.userId == user.Id))
                        {
                            db.Student_Groups.Where(x => x.groupId == a.group.Id && x.userId == user.Id).FirstOrDefault().inGroupStatus = true;
                        }
                        else
                        {
                            db.Student_Groups.Add(new GroupStudents { groupId = a.group.Id, userId = user.Id, inGroupStatus = true });
                        }

                        a.InvitationStatus = "Accepted";
                        a.CurrentAcceptedPeople += 1;
                        db.notifications.Add(new Notification
                        {
                            NotificationTheme = "Принятое приглашение",
                            NotificationText = "Пользователь" + user.lastName + " " + user.firstName + " " + user.secondOrFathersName + " " +
                            "вступил по приглашению в группу " + a.group.groupName,
                            UserToRecieveNot = a.group.teacher
                        });
                        db.Update(a);
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest("Неверный код приглашения");
                }



            }

            return Ok("Успешно");
        }
        [HttpPost("AddStudentToGroupByStudent")] //Api добавляющий студента в группу
        public IActionResult AddStudentToGroup([FromQuery] int groupid)
        {
            var t = Request.Headers["Authorization"].ToString();
            _timeService.LastVisitUpdate(t);
            User user = _userUtilityService.GetUserObjectFromToken(t);
            Group group = _groupService.GetGroupObjectFromGroupId(groupid);
            using (Primary_db_contex db = new Primary_db_contex(_configuration))
            {
                db.Student_Groups.Add(new GroupStudents { user = user, userId = user.Id, group = group, groupId = group.Id });
                db.SaveChanges();
            }
            return Ok();
        }
        [AllowAnonymous]
        [HttpGet("GetGroupFromGroupCred")] // Api возвращающий информацию о группе по ее уникальному индентификатору
        public IActionResult GetGroupFromGroupCred([FromQuery] int groupid)
        {
           // var t = Request.Headers["Authorization"].ToString();
            //_timeService.LastVisitUpdate(t);
            return Ok(_groupService.GetGroupObjectFromGroupId(groupid));

        }

        [HttpGet("GetUserGroupList")] //Возвращает список юзеров в группе
        public IActionResult GetUserGroupList()
        {
            var t = Request.Headers["Authorization"].ToString();
            _timeService.LastVisitUpdate(t);
            User user = _userUtilityService.GetUserObjectFromToken(t);
            using (Primary_db_contex db = new Primary_db_contex(_configuration))
            {
                List<Group> groups = new List<Group> { };
                foreach (var studentGroup in db.Student_Groups.Where(x => x.userId == user.Id))
                {
                    groups.Add(db.Groups.Where(x => x.Id == studentGroup.groupId).FirstOrDefault());
                }
                return Ok(groups);


            }

        }
        [HttpGet("GetUserGroupListCount")] //Возвращает количество юзеров в группе
        public IActionResult GetUserGroupCount([FromQuery] int groupid)
        {
            var t = Request.Headers["Authorization"].ToString();
            _timeService.LastVisitUpdate(t);
            User user = _userUtilityService.GetUserObjectFromToken(t);
            using (Primary_db_contex db = new Primary_db_contex(_configuration))
            {

                return Ok(db.Student_Groups.Where(x => x.groupId == groupid).Include(x => x.user).Count());

            }

        }
        [HttpPost("AddStudentToGroupById")]
        public IActionResult AddStudentToGroupById([FromQuery] int userid, int groupid)
        {
            var t = Request.Headers["Authorization"].ToString();
            _timeService.LastVisitUpdate(t);
            string role = _roleService.GetUserRole(t);
            if (role != "Teacher" && role != "Admin")
                return Unauthorized();
            using (Primary_db_contex db = new Primary_db_contex(_configuration))
            {
                db.Student_Groups.Add(new GroupStudents { userId = userid, groupId = groupid });
                db.SaveChanges();
            }
            return Ok();
        }
        [HttpPost("CreateNewGroup")] // Api Для создания новой группы
        public IActionResult GreateGroup([FromBody] Group group)
        {
            var t = Request.Headers["Authorization"].ToString();
            _timeService.LastVisitUpdate(t);
            string role = _roleService.GetUserRole(t);
            if (role != "Teacher" && role != "Admin")
                return Unauthorized();
            User user = _userUtilityService.GetUserObjectFromToken(t);


            using (Primary_db_contex db = new Primary_db_contex(_configuration))
            {
                group.teacher = db.users.Find(user.Id);
                group.lastUpdateTime = DateTime.Now;
                db.Groups.Add(group);
                db.SaveChanges();
            }
            return Ok();
        }
        [HttpPost("AddStudentToGroupCheckBox")]
        public IActionResult AddStudentToGroupTasksCheckBox([FromQuery] int groupId, int userId, bool Active)
        {
            using (Primary_db_contex db = new Primary_db_contex(_configuration))
            {
                if (Active)
                {
                    if (!db.Student_Groups.Any(x => x.group.Id == groupId && x.user.Id == userId))
                    {
                        GroupStudents a = new GroupStudents { groupId = groupId, userId = userId, inGroupStatus = true };
                        db.Student_Groups.Add(a);
                    }
                    else
                    {
                        db.Student_Groups.Where(x => x.group.Id == groupId && x.user.Id == userId).FirstOrDefault().inGroupStatus = true;
                    }
                }
                if (!Active)
                {
                    if (db.Student_Groups.Any(x => x.group.Id == groupId && x.userId == userId))
                    {
                        db.Student_Groups.Where(x => x.group.Id == groupId && x.user.Id == userId).FirstOrDefault().inGroupStatus = false;
                    }
                }
                db.SaveChanges();

            }
            return Ok();
        }
        [HttpPost("AddTaskToGroupTasksCheckBox")]
        public IActionResult AddTaskToGroupTasks([FromQuery] int groupId, int taskId, bool Active, int pointvalue)
        {
            using (Primary_db_contex db = new Primary_db_contex(_configuration))
            {
                int generalTaskPointValue = db.study_tasks.Find(taskId).taskPointValue;
                if (Active)
                {
                    if (!db.Group_Tasks.Any(x => x.group.Id == groupId && x.studyTask.Id == taskId))
                    {
                        GroupTask a = new GroupTask { groupId = groupId, studyTaskId = taskId, active = true, taskPointValueInGroup = generalTaskPointValue };
                        
                        if (pointvalue != 0)
                        {
                            //a.taskPointValueInGroup = pointvalue;
                        }

                        db.Group_Tasks.Add(a);
                       
                    }
                    else
                    {
                        db.Group_Tasks.Where(x => x.group.Id == groupId && x.studyTask.Id == taskId).FirstOrDefault().active = true;
                    }
                }
                if (!Active)
                {
                    if (db.Group_Tasks.Any(x => x.group.Id == groupId && x.studyTask.Id == taskId))
                    {
                        db.Group_Tasks.Where(x => x.group.Id == groupId && x.studyTask.Id == taskId).FirstOrDefault().active = false;
                    }
                }
                db.SaveChanges();

            }
            return Ok();
        }
        [HttpPost("AddTaskToGroupTasks")]
        public IActionResult AddTaskToGroupTasks([FromQuery] int groupId, int taskId)
        {
            using (Primary_db_contex db = new Primary_db_contex(_configuration))
            {
                if (!db.Group_Tasks.Any(x => x.group.Id == groupId && x.studyTask.Id == taskId))
                {
                    GroupTask a = new GroupTask { groupId = groupId, studyTaskId = taskId, active = true };
                    db.Group_Tasks.Add(a);
                    db.SaveChanges();
                }

            }
            return Ok();
        }
        [HttpPost("AddTaskToGroupTasksByName")]
        public IActionResult AddTaskToGroupTasksByName([FromQuery] int groupId, [FromBody] string taskName)
        {
            using (Primary_db_contex db = new Primary_db_contex(_configuration))
            {
                if (!db.Group_Tasks.Any(x => x.group == db.Groups.Find(groupId) && x.studyTask.task_name == taskName))
                {
                    GroupTask a = new GroupTask { group = db.Groups.Find(groupId), studyTask = db.study_tasks.Where(x => x.task_name == taskName).FirstOrDefault(), active = true };
                    db.Group_Tasks.Add(a);
                    db.SaveChanges();
                }

            }
            return Ok();
        }
        /*
        [HttpPost("GradeTask")]
        public IActionResult GradeTask([FromQuery] int groupId, int taskid, int userid, [FromBody] int pointValue)
        {
            var t = Request.Headers["Authorization"].ToString();
            _timeService.LastVisitUpdate(t);
            string role = _roleService.GetUserRole(t);
            if (role != "Teacher" && role != "Admin")
                return Unauthorized();
            using (Primary_db_contex db = new Primary_db_contex(_configuration))
            {

                var a = db.Users_tasks.Include(x => x.User).Where(x => x.User.Id == userid && x.Task.Id == taskid).FirstOrDefault();
                var b = db.Group_Tasks.Where(x => x.groupId == groupId && x.studyTaskId == taskid).FirstOrDefault().taskPointValueInGroup;
                if (!db.groupGrades.Any(x => x.groupId == groupId && x.userTaskId == a.Id))
                {
                    if (pointValue <= b)
                    {
                        db.groupGrades.Add(new GroupResults
                        {
                            userTaskId = a.Id,
                            groupId = groupId,
                            IsGraded = true,
                            maxPointGradeValue = b,
                            pointGradeValue = pointValue,
                        });
                        db.SaveChanges();
                        return Ok();
                    }

                    else
                        return BadRequest("Ваша оценка выше максимальной оценки за данное задание," +
                            " если вы хотите добавить дополнительный рейтинг сделайте это с помощью отдельной кнопки" +
                            " на странице оценивания   ");
                }
                else
                {
                    if (pointValue <= b)
                    {
                        db.groupGrades.Where(x => x.groupId == groupId && x.userTaskId == a.Id).FirstOrDefault().pointGradeValue = pointValue;
                        db.SaveChanges();
                        return Ok();
                    }

                    return BadRequest("Ваша оценка выше максимальной оценки за данное задание," +
                            " если вы хотите добавить дополнительный рейтинг сделайте это с помощью отдельной кнопки" +
                            " на странице оценивания   ");

                }

            }
        }
        /*
        [HttpGet("GetUserGrades")]
        public IActionResult GetUserGrades([FromQuery] int groupid, int userid)//Возвращает оценки ученика в определенной группе
        {
            var t = Request.Headers["Authorization"].ToString();
            _timeService.LastVisitUpdate(t);
            string role = _roleService.GetUserRole(t);
            using (Primary_db_contex db = new Primary_db_contex(_configuration))
            {
                try
                {
                    return Ok(db.groupGrades.Include(x => x.userTask.User).Where(x => x.IsGraded == true && x.userTask.UserId == userid && x.groupId == groupid).ToList());
                }
                catch (Exception ex)
                {
                    db.errorLogEntities.Add(new ErrorLogEntity
                    {
                        CausedException = ex.Message,
                        InFunction = "GetUserGrades",
                        userid = _userUtilityService.GetUserObjectFromToken(t).Id,
                        ErrorTime = DateTime.Now
                    }) ;
                    db.SaveChanges();
                    return BadRequest();
                }
                }


        }
        [HttpGet("GetGroupGrades")]
        public IActionResult GetGroupGrades([FromQuery] int groupid)//Возвращает оценки ученика в определенной группе
        {
            var t = Request.Headers["Authorization"].ToString();
            _timeService.LastVisitUpdate(t);
            string role = _roleService.GetUserRole(t);
            using (Primary_db_contex db = new Primary_db_contex(_configuration))
            {
                try
                {
                    return Ok(db.groupGrades.Include(x => x.userTask.User).Where(x => x.IsGraded == true &&  x.groupId == groupid).ToList());
                }
                catch (Exception ex)
                {
                    db.errorLogEntities.Add(new ErrorLogEntity
                    {
                        CausedException = ex.Message,
                        InFunction = "GetUserGrades",
                        userid = _userUtilityService.GetUserObjectFromToken(t).Id,
                        ErrorTime = DateTime.Now
                    });
                    db.SaveChanges();
                    return BadRequest();
                }
            }


        }
        [HttpPost("AddExtraCredit")]
        public IActionResult AddExtraCredit([FromQuery] int groupid, int userid, [FromBody] int pointValue)
        {
            var t = Request.Headers["Authorization"].ToString();
            _timeService.LastVisitUpdate(t);
            string role = _roleService.GetUserRole(t);
            if (role != "Teacher" && role != "Admin")
                return Unauthorized();
            using (Primary_db_contex db = new Primary_db_contex(_configuration))
            {
                try
                {
                    db.Student_Groups.Where(x => x.GroupId == groupid && x.UserId == userid && x.InGroupStatus == true).FirstOrDefault().GroupRating += pointValue;
                    db.SaveChanges();
                    return Ok();
                }
                catch (Exception ex)
                {
                    db.errorLogEntities.Add(new ErrorLogEntity
                    {
                        CausedException = ex.Message,
                        ErrorTime = DateTime.Now,
                        InFunction = "AddExtraCredit",
                        userid = _userUtilityService.GetUserObjectFromToken(t).Id
                    });
                    db.SaveChanges();
                    return BadRequest();
                }
            }
        }
        */
        [HttpGet]
        [HttpGet("GetAllTeacherGroupsInfoT")] // Возвращает список всех групп учителя 
        public IActionResult GetAllGroupInfo()
        {
            var t = Request.Headers["Authorization"].ToString();
            _timeService.LastVisitUpdate(t);
            string role = _roleService.GetUserRole(t);
            if (role != "Teacher" && role != "Admin")
                return Unauthorized();
            User user = _userUtilityService.GetUserObjectFromToken(t);
            using (Primary_db_contex db = new Primary_db_contex(_configuration))
            {
                return Ok(db.Groups.Where(x => x.teacher.username == db.users.Find(user.Id).username).ToList());
            }

        }

    }
}
