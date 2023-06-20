namespace TaskbookDesktopExtension
{
    partial class FindTaskBook
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FindTaskBook));
            this.button1 = new System.Windows.Forms.Button();
            this.pathToTaskBookTextBox = new System.Windows.Forms.TextBox();
            this.PathToTaskBookLabel = new System.Windows.Forms.Label();
            this.infoButton = new System.Windows.Forms.Button();
            this.directoryPathButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.button1.Location = new System.Drawing.Point(61, 117);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(587, 52);
            this.button1.TabIndex = 0;
            this.button1.Text = "Закрыть";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pathToTaskBookTextBox
            // 
            this.pathToTaskBookTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.pathToTaskBookTextBox.Location = new System.Drawing.Point(61, 85);
            this.pathToTaskBookTextBox.Name = "pathToTaskBookTextBox";
            this.pathToTaskBookTextBox.Size = new System.Drawing.Size(548, 26);
            this.pathToTaskBookTextBox.TabIndex = 1;
            // 
            // PathToTaskBookLabel
            // 
            this.PathToTaskBookLabel.AutoSize = true;
            this.PathToTaskBookLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.PathToTaskBookLabel.Location = new System.Drawing.Point(61, 52);
            this.PathToTaskBookLabel.Name = "PathToTaskBookLabel";
            this.PathToTaskBookLabel.Size = new System.Drawing.Size(269, 20);
            this.PathToTaskBookLabel.TabIndex = 2;
            this.PathToTaskBookLabel.Text = "Задачник установлен в папке:";
            // 
            // infoButton
            // 
            this.infoButton.Image = global::TaskbookDesktopExtension.Properties.Resources.question;
            this.infoButton.Location = new System.Drawing.Point(680, 154);
            this.infoButton.Name = "infoButton";
            this.infoButton.Size = new System.Drawing.Size(33, 26);
            this.infoButton.TabIndex = 9;
            this.infoButton.UseVisualStyleBackColor = true;
            // 
            // directoryPathButton
            // 
            this.directoryPathButton.Image = global::TaskbookDesktopExtension.Properties.Resources.folder1;
            this.directoryPathButton.Location = new System.Drawing.Point(615, 85);
            this.directoryPathButton.Name = "directoryPathButton";
            this.directoryPathButton.Size = new System.Drawing.Size(33, 26);
            this.directoryPathButton.TabIndex = 11;
            this.directoryPathButton.UseVisualStyleBackColor = true;
            this.directoryPathButton.Click += new System.EventHandler(this.directoryPathButton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(652, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(71, 67);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            // 
            // FindTaskBook
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(725, 192);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.directoryPathButton);
            this.Controls.Add(this.infoButton);
            this.Controls.Add(this.PathToTaskBookLabel);
            this.Controls.Add(this.pathToTaskBookTextBox);
            this.Controls.Add(this.button1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FindTaskBook";
            this.Text = "FindTaskBook";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FindTaskBook_FormClosing);
            this.Load += new System.EventHandler(this.FindTaskBook_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox pathToTaskBookTextBox;
        private System.Windows.Forms.Label PathToTaskBookLabel;
        private System.Windows.Forms.Button infoButton;
        private System.Windows.Forms.Button directoryPathButton;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}