using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace UI
{
    public partial class MainWinow : Form
    {
        private Controller controller=null;
        private CancellationTokenSource cts;
        private bool dirChosen = false;

        public MainWinow()
        {
            InitializeComponent();

        }

        private void FolderBrowseButton_Click(object sender, EventArgs e)
        {
            using (var browse = new FolderBrowserDialog())
            {
                if (browse.ShowDialog() == DialogResult.OK)
                {
                    PathTextBox.Text = browse.SelectedPath;
                    dirChosen = true;
                }
            }
            

        }

        private async void StartEncryptionButton_Click(object sender, EventArgs e)
        {           
            if (CheckPassword()==true && dirChosen==true)
            {
                if (controller == null)
                {                    
                    controller = new Controller();
                }
                
                cts = controller.CreateCancelationToken();
                var progressIndicator = controller.CreateProgress<int>(Report);
                           
                try
                {                    
                    label2.Visible = true;
                    label2.Text = "Zipping, please wait...";
                    await controller.CryptographyTask(progressIndicator, cts.Token, Tasks.Zip, PathTextBox.Text);
                    label2.Text = "Encrypting, please wait...";
                    await controller.CryptographyTask(progressIndicator, cts.Token, Tasks.Encrypt,String.Empty, Directory.GetFiles(PathTextBox.Text));
                    label2.Text = "Processing is done";                    
                }
                catch (OperationCanceledException)
                {
                    MessageBox.Show("Cancelled");
                }
                catch (Exception ex)
                {
                    MessageBox.Show( $"The process encountered a problem: {ex}");
                }
            }
           

        }
        public void Report(int value)
        {
           
            progressBar1.Value = value;
          
        }

        private bool CheckPassword(string salt=null)
        {   
            if(controller==null)
            {
                controller = new Controller();
            }
            bool passwordExists = false;
            if (Password_textBox.Text != null)
            {
                try
                {
                   
                    controller.CreatePassword(Password_textBox.Text, 256, salt);               
                    
                    passwordExists = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Wrong password data: {ex.ToString()}");
                }                
            }
            else
            {
                MessageBox.Show("You probably didn't enter the password");
            }
            return passwordExists;
        }

        private void Button1_Click(object sender, EventArgs e)
        {

            var browse = new OpenFileDialog();
            if (browse.ShowDialog() == DialogResult.OK)
            {
                controller.CompressAndWritePassWordHash(PathTextBox.Text);
            }
        }

        private async void StartDecryptionButton_Click(object sender, EventArgs e)
        {
                if (controller == null && dirChosen==true)
                {
                
                controller = new Controller(PathTextBox.Text);
                }
            
            bool condition1 = CheckPassword(controller.GetPasswordHashFromFile(PathTextBox.Text));
            bool condition2 = controller.ValidatePassword(PathTextBox.Text);


            if (condition1 == true && condition2== true )
            {                
                var progressIndicator = controller.CreateProgress<int>(Report);
                cts = controller.CreateCancelationToken();
                List<string> filesToDecrypt = new List<string>();
                try
                {
                    filesToDecrypt = controller.CheckHash(PathTextBox.Text);
                    label2.Visible = true;
                    label2.Text = "Decrypting, please wait...";
                    await controller.CryptographyTask(progressIndicator, cts.Token, Tasks.Decrypt, PathTextBox.Text, filesToDecrypt.ToArray());
                    label2.Text = "Decrypting is done";
                    if (File.Exists(PathTextBox.Text + @"\BufferFolder.rar"))
                    {                         
                        controller.UnzipDirectories(PathTextBox.Text);
                    }
                }
                catch (OperationCanceledException)
                {
                    MessageBox.Show("Cancelled");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                MessageBox.Show("Files are decrypted");
            }

            else
            {
                MessageBox.Show("Invalid password");
            }
        }      


        private void Form1_Load(object sender, EventArgs e)
        {            
            
        }      
                
        private void Button4_Click(object sender, EventArgs e)
        {
            cts.Cancel();
        }

      
    }
}
