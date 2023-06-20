using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Web;
using System.Text.RegularExpressions;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.IO;
using IWshRuntimeLibrary;

namespace TaskbookDesktopExtension
{
    public partial class TaskbookHelper : Form
    {
        private string pathToMainFolder;
        private string pathToTaskBookstr;
        public TaskbookHelper(string pathToTaskBookstr)
        {
            
            InitializeComponent();
            this.pathToTaskBookstr = pathToTaskBookstr;



        }
        public class SyncKey
        {
            public string key;
            public SyncKey(string key)
            {
                this.key = key;
            }
        }
        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void TaskbookHelper_Load(object sender, EventArgs e)
        {
            ToolTip keyLabelToolTip = new ToolTip();
            ToolTip infoButtonToolTip = new ToolTip();
            ToolTip catalogLabelToolTip = new ToolTip();
            pathToMainFolder = "C://";
            keyLabelToolTip.SetToolTip(keyLabel, "Ключ вы можете получить на сайте в своем личном кабинете");
            infoButtonToolTip.SetToolTip(infoButton, "Подробная помощь");
            catalogLabelToolTip.SetToolTip(catalogLabel, "Папка на вашем компьютере в которую вы хотите синхронизировать свои курсы");
            catalogTextBox.Text = "C://Taskbook";
            
        }
        public void ParseUserData(string data)
        {
            var a = data.Split('\"');
            //3
            //7
            //11
            //14
            loginLabel.Text = "Логин:";
            nameLabel.Text = "Имя:";
            surnameLabel.Text = "Фамилия:";
            loginLabel.Text = loginLabel.Text + " " + a[3];
            nameLabel.Text = nameLabel.Text + " " + a[7];
            surnameLabel.Text = surnameLabel.Text + " " + a[11];
            loginLabel.Enabled = true;
            nameLabel.Enabled = true;
            surnameLabel.Enabled = true;
            loginLabel.Visible = true;
            nameLabel.Visible = true;
            surnameLabel.Visible = true;


        }
        private void synchronizeButton_Click(object sender, EventArgs e)
        {
            if (keyTextBox.Text != "")
            {
                HttpClient client = new HttpClient();
                using (client)
                {
                    SyncKey key = new SyncKey(keyTextBox.Text);


                    client.BaseAddress = new Uri("https://localhost:44365/");
                    var response = client.PostAsJsonAsync("api/TaskBook/?key=" + key.key, key).Result;
                    var response2 = client.GetAsync("api/TaskBook/GetUserLocal?key=" + key.key).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var respRes = response.Content.ReadAsStringAsync();
                        var resp2Res = response2.Content.ReadAsStringAsync();
                        ParseUserData(resp2Res.Result.ToString());
                        CreateDirectories(ParseDirectoris(respRes.Result.ToString()));
                        foreach(var item in ParseDirectoris(respRes.Result.ToString()))
                        {
                            int id = int.Parse( item.Split("ID").Last());
                            var response3 = client.GetStreamAsync("api/TaskBook/SendAccessFile?key=" + key.key + "&groupId=" + id.ToString());
                            var response4 = client.GetStreamAsync("api/TaskBook/SendInitialResultFile?key=" + key.key + "&groupId=" + id.ToString() + "&path="+this.pathToTaskBookstr);
                            var resp3Res = response3.Result;
                            var resp4Res = response4.Result;
                            WshShell wshShell = new WshShell();
                            IWshShortcut Shortcut = (IWshShortcut)wshShell.CreateShortcut(pathToMainFolder + "/" + item.Split("ID")[0] + '/'+ "Load.lnk");
                            Shortcut.TargetPath = this.pathToTaskBookstr + '/' + "PT4Load.exe";
                            Shortcut.Save();
                            IWshShortcut Shortcut1 = (IWshShortcut)wshShell.CreateShortcut(pathToMainFolder + "/" + item.Split("ID")[0] + '/' + "Results.lnk");
                            Shortcut1.TargetPath = this.pathToTaskBookstr + '/' + "PT4Res.exe";
                            Shortcut1.Save();
                            IWshShortcut Shortcut2 = (IWshShortcut)wshShell.CreateShortcut(pathToMainFolder + "/" + item.Split("ID")[0] + '/' + "Demo.lnk");
                            Shortcut2.TargetPath = this.pathToTaskBookstr + '/' + "PT4Demo.exe";
                            Shortcut2.Save();
                            using (var fileStream = System.IO.File.Create(pathToMainFolder + "/"+ item.Split("ID")[0] + "/"+"access.dat"))
                            {

                                resp3Res.CopyTo(fileStream);
                            }
                            using (var fileStream = System.IO.File.Create(pathToMainFolder + "/" + item.Split("ID")[0] + "/" + "results.dat"))
                            {

                                resp4Res.CopyTo(fileStream);
                            }

                        }
                        MessageBox.Show("Синхронизация успешна!", "Успех!");
                        // keyTextBox.Text = a.Result.ToString().Trim();
                    }
                    else
                    {
                        string message = "Проверьте введенный ключ.\nЕсли вы ввели все правильно, попробуйте получить новый ключ в личном кабинете или обратиться в поддержку";
                        string caption = "Неверный ключ авторизации";
                        MessageBoxButtons buttons = MessageBoxButtons.OK;
                        MessageBoxIcon icon = MessageBoxIcon.Error;
                        MessageBox.Show(message, caption, buttons, icon);
                    }
                    

                }
            }
            else
            {
                string message = "Вы не ввели ключ авторизации";
                string caption = "Ошибка";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBoxIcon icon = MessageBoxIcon.Error;
                MessageBox.Show(message, caption, buttons, icon);
            }
            
        }

        private void infoButton_Click(object sender, EventArgs e)
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;
            using (FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog())
            {
                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    keyTextBox.Text = folderBrowserDialog1.SelectedPath;
                }
            }

        }
        public List<string> ParseDirectoris(string data)
        {
            data=data.Replace("[", "").Replace("]", "").Replace("\"", "");
            var a = data.Split(",");
            return a.ToList();
        }
        public void CreateDirectories(List<string> directoriesList)
        {
            foreach (var elem in directoriesList)
            {
                if (pathToMainFolder == "C://")
                    this.pathToMainFolder = pathToMainFolder + "Taskbook";
                Directory.CreateDirectory(this.pathToMainFolder +"/"+ elem.Split("ID")[0]);
            }
        }
        private void directoryPathButton_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog())
            {
                
                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    
                    this.pathToMainFolder = folderBrowserDialog1.SelectedPath;
                    catalogTextBox.Text = this.pathToMainFolder;
                }
            }
        }
    }
}
