using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace TaskbookDesktopExtension
{
    public partial class FindTaskBook : Form
    {
        public string pathToTaskBook ;
       
        public FindTaskBook()
        {
            InitializeComponent();
            this.pathToTaskBook = @"C:\Program Files (x86)\PT4";
            button1.Enabled = false;
        }
        public void TryFindTaskBook()
        {
            if (Directory.Exists(pathToTaskBook))
            {
                if (File.Exists(pathToTaskBook+'/'+"PT4Load.exe"))
                {
                    MessageBox.Show("Задачник найден!\nВы можете закрыть данное окно","Успешно!",MessageBoxButtons.OKCancel);
                    button1.Enabled = true;
                    pathToTaskBookTextBox.Text = this.pathToTaskBook;
                    
                }
                else
                    MessageBox.Show("Исполняемый файл задачника не был найден!\n Пожалуйста, укажите правильный путь к папке с задачником","Внимание!",MessageBoxButtons.OKCancel,MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Исполняемый файл задачника не был найден!\n Пожалуйста, укажите правильный путь к папке с задачником", "Внимание!", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

        }

        private void button1_Click(object sender, EventArgs e)
        {

            Hide();
            Form f2 = new TaskbookHelper(pathToTaskBook);
            f2.ShowDialog();

        }

        private void FindTaskBook_FormClosing(object sender, FormClosingEventArgs e)
        {
            
            
            
        }

        private void FindTaskBook_Load(object sender, EventArgs e)
        {
            TryFindTaskBook();
        }

        private void directoryPathButton_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog())
            {

                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {

                    this.pathToTaskBook = folderBrowserDialog1.SelectedPath;
                    pathToTaskBookTextBox.Text = this.pathToTaskBook;
                    TryFindTaskBook();
                }
            }
        }
    }
}
