using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace AES256FileEncryption
{


    public partial class Form1 : Form
    {
        private AEScryptography AEScrypto;
        private LogicalListener listener;
        private CancellationTokenSource cts;

        public Form1()
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
                }
            }

        }

        private async void StartEncryptionButton_Click(object sender, EventArgs e)
        {
//            HashStoringFile hf = new HashStoringFile(PathTextBox.Text);
            
        //    listener.OnDirectoryChosen(PathTextBox.Text);
            //construct Progress<T>, passing ReportProgress as the Action<T>
             cts = new CancellationTokenSource();
           
            var progressIndicator = new Progress<int>(Report);
            //call async method
            try
            {
                //                MessageBox.Show(Directory.GetFiles(PathTextBox.Text));
                label1.Visible = true;
                label1.Text = "Zipping, please wait...";
                await listener.EncryptBytesAsync(progressIndicator, cts.Token, "Zip",PathTextBox.Text);
                label1.Text = "Encrypting, please wait...";
                await listener.EncryptBytesAsync(progressIndicator, cts.Token, "Encrypt","",Directory.GetFiles(PathTextBox.Text));
                label1.Text = "Processing is done";
               // progressBar1.Value = 0;
            }
            catch (OperationCanceledException ex)
            {
               
                MessageBox.Show(ex.Message, "Cancelled");
            }
           

        }
        public void Report(int value)
        {
           
            progressBar1.Value = value;
          
        }

        private void Button1_Click(object sender, EventArgs e)
        {

            var browse = new OpenFileDialog();
            if (browse.ShowDialog() == DialogResult.OK)
            {
                listener.OnDirectoryChosen(PathTextBox.Text);
            }
        }

        private async void StartDecryptionButton_Click(object sender, EventArgs e)
        {

            foreach (var file in Directory.GetFiles(PathTextBox.Text).Where(name => !name.EndsWith("HashValues.txt")))
            {
                var progressIndicator = new Progress<int>(Report);
                MD5HASH hash = new MD5HASH(file);
                string encFileHash = hash.BeginHashing();
                var filesToDecrypt = new List<string>();
                if (CheckDecriptingFileHash(encFileHash))
                {
                   // byte[] decrypted = AEScrypto.Decrypt(File.ReadAllBytes(file));
                   // File.WriteAllBytes(PathTextBox.Text + @"\" + Path.GetFileName(file), decrypted);
                    filesToDecrypt.Add(file);
                    //if (!file.EndsWith("BuffZip.rar")) continue;
                   
                }
                else
                {
                    MessageBox.Show($"Failas, kurio parašas yra: {encFileHash} - pakeistas!");
                }
               // cts = new CancellationTokenSource();
                
                try
                {

                await listener.EncryptBytesAsync(progressIndicator, cts.Token, "Decrypt", PathTextBox.Text, filesToDecrypt.ToArray());
               
                if (File.Exists(PathTextBox.Text + @"\BuffZip.rar"))
                {
                     Zipper zip = new Zipper(PathTextBox.Text);
                     zip.ExtractDirectories();
                }
            }
            catch (OperationCanceledException ex)
            {
                MessageBox.Show(ex.Message, "Cancelled");
            }

        }

            MessageBox.Show("Failai atkoduoti");

        }

        private bool CheckDecriptingFileHash(string file)
        {

            using (StreamReader reader = new StreamReader(PathTextBox.Text + @"\" + "HashValues.txt"))
            {
                string content = reader.ReadToEnd();
                if (content.Contains(file))
                {
                    reader.Close();
                    return true;
                }
            }
            return false;
        }


        private void Button2_Click(object sender, EventArgs e)
        {

        }

        private void Button3_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            AEScrypto = new AEScryptography(256);
            AEScrypto.PasswordHandler += OnPasswordGenerated;
            listener = new LogicalListener(AEScrypto);


        }

        public void OnPasswordGenerated(object sender, PasswordArgs e)
        {

            //listBox1.Items.Add(e.password);
        }

        private void PasswordShowBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void BackgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
          
        }

        private void BackgroundWorker1_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {

        }

        private void BackgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            
           byte[] encrypted= AEScrypto.Encrypt(File.ReadAllBytes(PathTextBox.Text + @"\" + "HashValues.txt"), "ds");
           File.WriteAllBytes(PathTextBox.Text + @"\" + "HashValues.txt",encrypted);
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            cts.Cancel();
        }
    }
}
