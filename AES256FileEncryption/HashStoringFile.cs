using System;
using System.IO;
using System.Text;

namespace AES256FileEncryption
{
    class HashStoringFile
    {
        private string filePath;
        // private LogicalListener listener;


        public HashStoringFile(string filePath)
        {
            this.filePath = filePath;

            //  listener.HashHandler += OnFileEncrypted;
        }

        private void CreateHashFile(byte[] bytes)
        {
            if (!File.Exists(filePath + @"\HashValues.txt"))
            {
                using (FileStream fs = new FileStream(filePath + @"\HashValues.txt", FileMode.Create, FileAccess.Write))
                {
                    fs.Write(bytes, 0, bytes.Length);

                }

            }

            else
                using (FileStream fs = new FileStream(filePath + @"\HashValues.txt", FileMode.Append, FileAccess.Write, FileShare.Write))
                {

                    fs.Write(bytes, 0, bytes.Length);

                }

        }

        //        public void OnFileEncrypted(object source, HashArgs e)
        //        {
        //           MessageBox.Show("lol");
        //            MD5HASH hash = new MD5HASH(e.fileName);
        //            string fileName = Path.GetFileName(e.fileName);
        //            string fileHash = hash.BeginHashing();
        //            byte[] stringBytes = Encoding.UTF8.GetBytes(fileName + " " + fileHash + Environment.NewLine);
        //            CreateHashFile(stringBytes);
        //
        //        }
        public void OnFileEncrypted(string file)
        {

            MD5HASH hash = new MD5HASH(file);
            string fileName = Path.GetFileName(file);
            string fileHash = hash.BeginHashing();
            byte[] stringBytes = Encoding.UTF8.GetBytes(fileName + " " + fileHash + Environment.NewLine);
            CreateHashFile(stringBytes);

        }

    }
}
