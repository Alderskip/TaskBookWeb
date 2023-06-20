using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
namespace TaskbookServer.Services

{
    public class ParserForTaskGroupsService
    {
        
        public void test()
        {
            string filePath = "../Tasks/abramyan_pttasks.pdf";

            Parser parser = new Parser();
            List<Group1> groups = parser.ParsePdf(filePath);

            foreach (Group1 group in groups)
            {
                Console.WriteLine("Group: " + group.Name);
                Console.WriteLine("Class Parsing: " + string.Join(", ", group.ClassParsing));
                Console.WriteLine("Material Reinforcement: " + string.Join(", ", group.MaterialReinforcement));
                Console.WriteLine("Simple Tasks: " + string.Join(", ", group.SimpleTasks));
                Console.WriteLine("Complex Tasks: " + string.Join(", ", group.ComplexTasks));
                Console.WriteLine("Task Sets: " + string.Join("; ", group.TaskSets));
                
            }
        }
        private readonly IConfiguration _configuration;
        public ParserForTaskGroupsService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public class Group1
        {
            public string Name { get; set; }
            public List<int> ClassParsing { get; set; }
            public List<int> MaterialReinforcement { get; set; }
            public List<int> SimpleTasks { get; set; }
            public List<int> ComplexTasks { get; set; }
            public List<string> TaskSets { get; set; }
        }

        public class Parser
        {
            public List<Group1> ParsePdf(string filePath)
            {
                List<Group1> groups = new List<Group1>();

                using (PdfReader reader = new PdfReader(filePath))
                {
                    for (int page = 1; page <= reader.NumberOfPages; page++)
                    {
                        string content = PdfTextExtractor.GetTextFromPage(reader, page);
                        Group1 group = ExtractGroup(content);

                        if (group != null)
                        {
                            groups.Add(group);
                        }
                    }
                }

                return groups;
            }

            private Group1 ExtractGroup(string content)
            {
                Group1 group = new Group1();

                // Check if the construction exists on the page
                bool constructionExists = content.Contains("Группа:");
                if (!constructionExists)
                {
                    return null;
                }

                // Extract "Группа" name
                int groupIndex = content.IndexOf("Группа:");
                if (groupIndex != -1)
                {
                    int nextLineIndex = content.IndexOf('\n', groupIndex);
                    if (nextLineIndex != -1)
                    {
                        group.Name = content.Substring(groupIndex + 7, nextLineIndex - groupIndex - 7).Trim();
                    }
                }

                // Extract other information
                group.ClassParsing = ExtractNumbers(content, "Разбор в классе:");
                group.MaterialReinforcement = ExtractNumbers(content, "Закрепление материала:");
                group.SimpleTasks = ExtractNumbers(content, "Простые задания:");
                group.ComplexTasks = ExtractNumbers(content, "Сложные задания:");
                group.TaskSets = ExtractTaskSets(content, "Наборы однотипных заданий:");

                return group;
            }

            private List<int> ExtractNumbers(string content, string keyword)
            {
                List<int> numbers = new List<int>();

                int startIndex = content.IndexOf(keyword);
                if (startIndex != -1)
                {
                    int endIndex = content.IndexOf('\n', startIndex);
                    if (endIndex != -1)
                    {
                        string numbersString = content.Substring(startIndex + keyword.Length, endIndex - startIndex - keyword.Length);

                        // Split the numbers string by comma and remove any leading/trailing whitespace
                        string[] numberStrings = numbersString.Split(',')
                                                              .Select(n => n.Trim())
                                                              .ToArray();

                        foreach (string numberString in numberStrings)
                        {
                            if (int.TryParse(numberString, out int number))
                            {
                                numbers.Add(number);
                            }
                        }
                    }
                }

                return numbers;
            }

            private List<string> ExtractTaskSets(string content, string keyword)
            {
                List<string> taskSets = new List<string>();

                int startIndex = content.IndexOf(keyword);
                if (startIndex != -1)
                {
                    int endIndex = content.IndexOf('\n', startIndex);
                    if (endIndex != -1)
                    {
                        string taskSetsString = content.Substring(startIndex + keyword.Length, endIndex - startIndex - keyword.Length);
                        taskSets = taskSetsString.Split(';').Select(t => t.Trim()).ToList();
                    }
                }

                return taskSets;
            }
            
        }
    }
}
