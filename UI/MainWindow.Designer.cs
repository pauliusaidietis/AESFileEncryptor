namespace UI
{
    partial class MainWinow
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
            this.FolderBrowseButton = new System.Windows.Forms.Button();
            this.PathTextBox = new System.Windows.Forms.TextBox();
            this.StartEncryptionButton = new System.Windows.Forms.Button();
            this.StartDecryptionButton = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.button4 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.Password_textBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // FolderBrowseButton
            // 
            this.FolderBrowseButton.Location = new System.Drawing.Point(559, 27);
            this.FolderBrowseButton.Margin = new System.Windows.Forms.Padding(4);
            this.FolderBrowseButton.Name = "FolderBrowseButton";
            this.FolderBrowseButton.Size = new System.Drawing.Size(100, 28);
            this.FolderBrowseButton.TabIndex = 0;
            this.FolderBrowseButton.Text = "Browse...";
            this.FolderBrowseButton.UseVisualStyleBackColor = true;
            this.FolderBrowseButton.Click += new System.EventHandler(this.FolderBrowseButton_Click);
            // 
            // PathTextBox
            // 
            this.PathTextBox.Location = new System.Drawing.Point(13, 30);
            this.PathTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.PathTextBox.Name = "PathTextBox";
            this.PathTextBox.Size = new System.Drawing.Size(538, 22);
            this.PathTextBox.TabIndex = 1;
            // 
            // StartEncryptionButton
            // 
            this.StartEncryptionButton.Location = new System.Drawing.Point(13, 170);
            this.StartEncryptionButton.Margin = new System.Windows.Forms.Padding(4);
            this.StartEncryptionButton.Name = "StartEncryptionButton";
            this.StartEncryptionButton.Size = new System.Drawing.Size(100, 28);
            this.StartEncryptionButton.TabIndex = 2;
            this.StartEncryptionButton.Text = "Encrypt";
            this.StartEncryptionButton.UseVisualStyleBackColor = true;
            this.StartEncryptionButton.Click += new System.EventHandler(this.StartEncryptionButton_Click);
            // 
            // StartDecryptionButton
            // 
            this.StartDecryptionButton.Location = new System.Drawing.Point(121, 170);
            this.StartDecryptionButton.Margin = new System.Windows.Forms.Padding(4);
            this.StartDecryptionButton.Name = "StartDecryptionButton";
            this.StartDecryptionButton.Size = new System.Drawing.Size(100, 28);
            this.StartDecryptionButton.TabIndex = 4;
            this.StartDecryptionButton.Text = "Decrypt";
            this.StartDecryptionButton.UseVisualStyleBackColor = true;
            this.StartDecryptionButton.Click += new System.EventHandler(this.StartDecryptionButton_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(13, 134);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(4);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(643, 28);
            this.progressBar1.TabIndex = 9;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(556, 170);
            this.button4.Margin = new System.Windows.Forms.Padding(4);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(100, 28);
            this.button4.TabIndex = 10;
            this.button4.Text = "Cancel";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.Button4_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 265);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 17);
            this.label1.TabIndex = 11;
            this.label1.Visible = false;
            // 
            // Password_textBox
            // 
            this.Password_textBox.Location = new System.Drawing.Point(93, 73);
            this.Password_textBox.Name = "Password_textBox";
            this.Password_textBox.PasswordChar = '*';
            this.Password_textBox.Size = new System.Drawing.Size(212, 22);
            this.Password_textBox.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 17);
            this.label3.TabIndex = 14;
            this.label3.Text = "Password: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 113);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 17);
            this.label2.TabIndex = 15;
            this.label2.Text = "label2";
            this.label2.Visible = false;
            // 
            // MainWinow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(675, 258);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Password_textBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.StartDecryptionButton);
            this.Controls.Add(this.StartEncryptionButton);
            this.Controls.Add(this.PathTextBox);
            this.Controls.Add(this.FolderBrowseButton);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainWinow";
            this.Text = "Crypto";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button FolderBrowseButton;
        private System.Windows.Forms.TextBox PathTextBox;
        private System.Windows.Forms.Button StartEncryptionButton;
        private System.Windows.Forms.Button StartDecryptionButton;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox Password_textBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
    }
}

