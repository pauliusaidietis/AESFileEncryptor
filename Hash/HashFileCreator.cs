using System;
using System.IO;
using System.Text;

namespace Hash
{
    public class HashFileCreator : ICreateHashFile, IContainHashFile
    {
        private string filePath;
        public string HashFileName { get; set; } = @"\HashValues.txt";

        public HashFileCreator(string filePath)
        {
            this.filePath = filePath;
                        
        }

        public void CreateHashFile(byte[] bytes)
        {
            if (!File.Exists(filePath + HashFileName))
            {
                using (FileStream fs = new FileStream(filePath + HashFileName, FileMode.Create, FileAccess.Write))
                {
                    fs.Write(bytes, 0, bytes.Length);
                    
                }

            }
            else
                using (FileStream fs = new FileStream(filePath + HashFileName, FileMode.Append, FileAccess.Write, FileShare.Write))
                {
                    fs.Write(bytes, 0, bytes.Length);
                }

        }
              

        public void TransformStringAndWrite(string fileName, string fileHash)
        {
            byte[] stringBytes = Encoding.UTF8.GetBytes(fileName + " " + fileHash + Environment.NewLine);
            CreateHashFile(stringBytes);            
        }

        public void TransfromBase64StringAndWrite(string fileName, string fileHash)
        {            
            File.WriteAllText(filePath + HashFileName, fileName + ":" + fileHash + Environment.NewLine, Encoding.GetEncoding("utf-8"));
            
        }

    }
}
