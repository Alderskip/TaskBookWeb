
namespace TaskbookDesktopExtension
{
    partial class TaskbookHelper
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TaskbookHelper));
            this.keyLabel = new System.Windows.Forms.Label();
            this.keyTextBox = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.synchronizeButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.loginLabel = new System.Windows.Forms.Label();
            this.exitButton = new System.Windows.Forms.Button();
            this.infoButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.surnameLabel = new System.Windows.Forms.Label();
            this.nameLabel = new System.Windows.Forms.Label();
            this.directoryPathButton = new System.Windows.Forms.Button();
            this.catalogLabel = new System.Windows.Forms.Label();
            this.catalogTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // keyLabel
            // 
            this.keyLabel.AutoSize = true;
            this.keyLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.keyLabel.Location = new System.Drawing.Point(2, 205);
            this.keyLabel.Name = "keyLabel";
            this.keyLabel.Size = new System.Drawing.Size(76, 25);
            this.keyLabel.TabIndex = 0;
            this.keyLabel.Text = "Ключ:";
            // 
            // keyTextBox
            // 
            this.keyTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.keyTextBox.Location = new System.Drawing.Point(166, 205);
            this.keyTextBox.Name = "keyTextBox";
            this.keyTextBox.Size = new System.Drawing.Size(403, 26);
            this.keyTextBox.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(586, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(98, 96);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Gainsboro;
            this.label2.Font = new System.Drawing.Font("Cambria", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(166, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(400, 57);
            this.label2.TabIndex = 3;
            this.label2.Text = "Taskbook Helper";
            // 
            // synchronizeButton
            // 
            this.synchronizeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.synchronizeButton.Location = new System.Drawing.Point(166, 237);
            this.synchronizeButton.Name = "synchronizeButton";
            this.synchronizeButton.Size = new System.Drawing.Size(442, 31);
            this.synchronizeButton.TabIndex = 4;
            this.synchronizeButton.Text = "Синхронизировать";
            this.synchronizeButton.UseVisualStyleBackColor = true;
            this.synchronizeButton.Click += new System.EventHandler(this.synchronizeButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(3, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(118, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "Пользователь:";
            // 
            // loginLabel
            // 
            this.loginLabel.AutoSize = true;
            this.loginLabel.Enabled = false;
            this.loginLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.loginLabel.Location = new System.Drawing.Point(3, 38);
            this.loginLabel.Name = "loginLabel";
            this.loginLabel.Size = new System.Drawing.Size(59, 20);
            this.loginLabel.TabIndex = 6;
            this.loginLabel.Text = "Логин:";
            this.loginLabel.Visible = false;
            // 
            // exitButton
            // 
            this.exitButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.exitButton.Location = new System.Drawing.Point(12, 270);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(78, 34);
            this.exitButton.TabIndex = 7;
            this.exitButton.Text = "Выход";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // infoButton
            // 
            this.infoButton.Image = global::TaskbookDesktopExtension.Properties.Resources.question;
            this.infoButton.Location = new System.Drawing.Point(575, 205);
            this.infoButton.Name = "infoButton";
            this.infoButton.Size = new System.Drawing.Size(33, 26);
            this.infoButton.TabIndex = 8;
            this.infoButton.UseVisualStyleBackColor = true;
            this.infoButton.Click += new System.EventHandler(this.infoButton_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.surnameLabel);
            this.panel1.Controls.Add(this.nameLabel);
            this.panel1.Controls.Add(this.loginLabel);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new System.Drawing.Point(2, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(158, 136);
            this.panel1.TabIndex = 9;
            // 
            // surnameLabel
            // 
            this.surnameLabel.AutoSize = true;
            this.surnameLabel.Enabled = false;
            this.surnameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.surnameLabel.Location = new System.Drawing.Point(3, 110);
            this.surnameLabel.Name = "surnameLabel";
            this.surnameLabel.Size = new System.Drawing.Size(85, 20);
            this.surnameLabel.TabIndex = 8;
            this.surnameLabel.Text = "Фамилия:";
            this.surnameLabel.Visible = false;
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Enabled = false;
            this.nameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.nameLabel.Location = new System.Drawing.Point(3, 76);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(44, 20);
            this.nameLabel.TabIndex = 7;
            this.nameLabel.Text = "Имя:";
            this.nameLabel.Visible = false;
            // 
            // directoryPathButton
            // 
            this.directoryPathButton.Image = global::TaskbookDesktopExtension.Properties.Resources.folder1;
            this.directoryPathButton.Location = new System.Drawing.Point(575, 168);
            this.directoryPathButton.Name = "directoryPathButton";
            this.directoryPathButton.Size = new System.Drawing.Size(33, 26);
            this.directoryPathButton.TabIndex = 10;
            this.directoryPathButton.UseVisualStyleBackColor = true;
            this.directoryPathButton.Click += new System.EventHandler(this.directoryPathButton_Click);
            // 
            // catalogLabel
            // 
            this.catalogLabel.AutoSize = true;
            this.catalogLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.catalogLabel.Location = new System.Drawing.Point(2, 168);
            this.catalogLabel.Name = "catalogLabel";
            this.catalogLabel.Size = new System.Drawing.Size(159, 24);
            this.catalogLabel.TabIndex = 11;
            this.catalogLabel.Text = "Рабочая папка:";
            // 
            // catalogTextBox
            // 
            this.catalogTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.catalogTextBox.Location = new System.Drawing.Point(166, 168);
            this.catalogTextBox.Name = "catalogTextBox";
            this.catalogTextBox.Size = new System.Drawing.Size(403, 26);
            this.catalogTextBox.TabIndex = 12;
            // 
            // TaskbookHelper
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(696, 316);
            this.ControlBox = false;
            this.Controls.Add(this.catalogTextBox);
            this.Controls.Add(this.catalogLabel);
            this.Controls.Add(this.directoryPathButton);
            this.Controls.Add(this.infoButton);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.synchronizeButton);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.keyTextBox);
            this.Controls.Add(this.keyLabel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TaskbookHelper";
            this.Text = "Taskbook Helper";
            this.Load += new System.EventHandler(this.TaskbookHelper_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label keyLabel;
        private System.Windows.Forms.TextBox keyTextBox;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button synchronizeButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label loginLabel;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.Button infoButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label surnameLabel;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.Button directoryPathButton;
        private System.Windows.Forms.Label catalogLabel;
        private System.Windows.Forms.TextBox catalogTextBox;
    }
}

