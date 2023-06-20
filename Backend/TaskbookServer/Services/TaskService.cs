using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using TaskbookServer.Models;

namespace TaskbookServer.Services
{
    public class TaskService
    {
        private readonly IConfiguration _configuration;
        public TaskService(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public string DetermineTaskType(int taskNumber, Dictionary<string, int[]> tasksComposition)
        {
            foreach (var taskComposition in tasksComposition)
            {
                if (taskComposition.Value.Contains(taskNumber))
                {
                    return taskComposition.Key;
                }
                
            }
            return "Variant";
          
        }
        public string DetermineTaskDifficulty(int taskNumber,Dictionary<string, int[]> hardAndEasyTasks)
        {
            foreach (var task in hardAndEasyTasks)
            {
                if (task.Value.Contains(taskNumber))
                {
                    switch (task.Key)
                    {
                        case "Easy":
                            {
                                return "Easy";
                            }
                        case "Hard":
                            {
                                return  "Hard";
                            }
                        default:
                            {
                                return "Default";
                            }

                    }
                }

            }
            return "Default";
        }
    

        public double DetermineTaskPointValueCoaf(int taskNumber, Dictionary<string, int[]> tasksComposition)
        {
            foreach (var taskComposition in tasksComposition)
            {
                if (taskComposition.Value.Contains(taskNumber))
                {
                    switch (taskComposition.Key)
                    {
                        case "Primary":
                            {
                                return 1;
                            }
                        case "Easy":
                            {
                                return 0.5;
                            }
                        case "Hard":
                            {
                                return  1.5;
                            }
                        case "Secondary":
                            {
                                return 0.8;
                            }
                        
                        default:
                            {
                                return 1;
                            }

                    }
                }
              
            }
            return 1;
        }



        public void parseStudyTaskGroup(
            string fileName,
            string taskGroupName,
            int numberOfTasks,
            Dictionary<string,int[]> tasksComposition, 
            Dictionary<string,int[]> hardAndEasyTasks,
            int standartGroupPointValue )
        {
            var studyTasks = new List<StudyTask>();
            int taskCounter = 1;
            var taskLines = System.IO.File.ReadAllLines("../Tasks/"+fileName);
            StudyTaskGroup taskGroup = new StudyTaskGroup { studyTaskGroupName = taskGroupName, studyTaskGroupCode = "" };

            using (Primary_db_contex db = new Primary_db_contex(_configuration))
            {

                db.studyTaskGroups.Add(taskGroup);
                db.SaveChanges();
                taskGroup = db.studyTaskGroups.Where(x => x.studyTaskGroupName == taskGroupName).FirstOrDefault();


                foreach (var taskLine in taskLines)
                {
                    db.study_tasks.Add(new StudyTask
                    {
                        taskName = taskGroupName + taskCounter.ToString(),
                        taskDesc = taskLine.Split(".",2)[1].Trim().Split("</p>")[0],
                        studyTaskType= DetermineTaskType(taskCounter,tasksComposition),
                        taskPointValue = standartGroupPointValue * 
                        (int)DetermineTaskPointValueCoaf(taskCounter,tasksComposition) * 
                        (int)DetermineTaskPointValueCoaf(taskCounter, hardAndEasyTasks),
                        taskDifficulty= DetermineTaskDifficulty(taskCounter,hardAndEasyTasks),
                        studyTaskGroupId =taskGroup.Id,
                    });
                    taskCounter++;
                    db.SaveChanges();
                }
            }
        }


        public void parseBeginStudyTask()
        {
            var studyTasks = new List<StudyTask>();
            int taskCounter = 1;
            

            var elem = System.IO.File.ReadAllLines("../Tasks/BeginTasksHTML.txt");
            StudyTaskGroup TaskGroup = new StudyTaskGroup { studyTaskGroupName = "Begin", studyTaskGroupCode = "" };
            foreach (var elem1 in elem)
            {
               
               //studyTasks.Add(new StudyTask {studyTaskGroup=TaskGroup,taskName="Begin"+taskCounter.ToString(),taskDesc=elem1.Split(".",1)[0],studyTaskType=TaskType.Hard})
              

            }


        }
        public int GetStudyTaskPoint(string TaskName)
        {
            switch (TaskName)
            {
                case "Begin":
                    {
                        return 1;
                        
                    }
                case "For":
                    {
                        return 20;

                    }
                case "Boolean":
                    {
                        return 2;

                    }
                case "Case":
                    {
                        return 1;

                    }
                case "Integer":
                    {
                        return 5;

                    }
                case "Minmax":
                    {
                        return 10;

                    }
                default:return 10;

            }
        }
    }
}
