
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Npgsql;
using TaskbookServer.Models;

namespace TaskbookServer.Services
{

    public class ResultService 
    {
        private readonly IConfiguration _configuration;
        public ResultService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string StringOfCompletedTask(User user) // Функция получения строки с завершенными заданиями ученика
        {
            using (Primary_db_contex db = new Primary_db_contex(_configuration))
            {
                var a = db.study_task_results.Where(x => x.User == user && x.result == "Задание выполнено!");
                string res = "";
                foreach (var item in a)
                {
                    string subsring = item.FullResultLine.Split(" ")[0];
                    if (!res.Contains(subsring))
                        res = res + " " + subsring;
                }

                return res;
            }

        }
        public List<UserTask> GetListOfUserCompletedTask(User user) //Функция Возвращающая список выполненных пользователем заданий типа UserTask
        {
            using (Primary_db_contex db = new Primary_db_contex(_configuration))
            {
                return db.Users_tasks.Where(x => x.User.Id == user.Id && x.Completed==true).ToList();
            }
        }
        public List<UserTask> GetListOfUserInProgressTask(User user) //Функция возвращающая список не выполненных пользоватлем заданий типа UserTask
        {
            using (Primary_db_contex db = new Primary_db_contex(_configuration))
            {
                return db.Users_tasks.Where(x => x.User.Id == user.Id && x.Completed != true).ToList();
            }
        }
        public void UpdateUserTasksInfo(User user,StudyTaskResult line)
        {
            using (Primary_db_contex db = new Primary_db_contex(_configuration))
            {
                if (db.Users_tasks.Any(x => x.User == user && x.Task == line.StudyTask))
                {
                    if (db.Users_tasks.Where(x => x.User == user && x.Task == line.StudyTask).FirstOrDefault().Completed == false && line.result == "Задание выполнено!")
                    {
                        db.Users_tasks.Where(x => x.User ==db.users.Find(user.Id)&& x.Task == db.study_tasks.Find(line.StudyTask.Id)).FirstOrDefault().Completed = true;
                        db.users.Find(user.Id).totalStudyTaskPoints += line.StudyTask.taskPointValue;
                        db.SaveChanges();
                    }

                }
                else
                {
                    
                   
                    db.Users_tasks.Add(new UserTask{User=db.users.Find(user.Id), Task=db.study_tasks.Find(line.StudyTask.Id), Completed=false });
                    db.SaveChanges();
                }
                    
               

            }


        }
            /*
        public void UpdateUserTasksInfo(User user)
        {
            
            using (Primary_db_contex db = new Primary_db_contex(_configuration))
            {
                List<StudyTaskResult> a = db.study_task_results.Where(x => x.User == user && x.result == "Задание выполнено!").ToList();
                List<StudyTask> c = new List<StudyTask> { };
                foreach (var item in a)
                {
                    c.Add(item.StudyTask);
                }
                List<StudyTaskResult> b = db.study_task_results.Where(x => x.User == user && x.result != "Задание выполнено!").ToList();
                foreach (StudyTaskResult item in b)
                {
                    if (c.Contains(item.StudyTask))
                        b.Remove(item);
                        
                }
                
                foreach (var item in b)
                {
                    if (!db.Users_tasks.Any(x => x.User == user && x.Task == item.StudyTask))
                    {
                        db.Users_tasks.Add(new UserTask { User = user, Task = item.StudyTask, Completed = false });
                        db.SaveChanges();
                    }
                    
                }
                foreach (var item in a)
                {
                    db.Users_tasks.Where(x => x.User == user && x.Task == item.StudyTask && x.Completed == false).FirstOrDefault().Completed = true;
                    db.SaveChanges();

                }
                
            }

        }
            */
        public string StringOfInprogressTask(User user, string CompletedTasks) //Функция получения строки с заданиями в прогрессе ученика
        {
            using (Primary_db_contex db = new Primary_db_contex(_configuration))
            {
                var a = db.study_task_results.Where(x => x.User == user && x.result != "Задание выполнено!");
                string res = "";
                foreach (var item in a)
                {
                    string subsring = item.FullResultLine.Split(" ")[0];
                    if (!res.Contains(subsring) && !CompletedTasks.Contains(subsring))
                        res = res + " " + subsring;
                }

                return res;
            }
        }
        public double GetRaiting(User user, string CompletedTask, string InprogressTask) // Функция для вычесления рейтинга ученика 
        {
            string AllTasks = CompletedTask + InprogressTask;
            var substring = AllTasks.Trim().Split(" ");
            double a = substring.Length;
            substring = CompletedTask.Trim().Split(" ");
            double b = substring.Length;
            double measurment = (b / a) * 100;

            return measurment;
        }
    }
  
}