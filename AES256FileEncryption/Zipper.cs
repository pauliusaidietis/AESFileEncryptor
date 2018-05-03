using System;
using System.IO;
using System.IO.Compression;


namespace AES256FileEncryption
{
    class Zipper
    {
        private string pathDir;

        public Zipper(string pathDir)
        {
            this.pathDir = pathDir;
        }


        private string CheckForDirectories()
        {
            string[] directoryCount = Directory.GetDirectories(pathDir);
            int length = directoryCount.Length;

            if (length >= 1)
            {
                var bufferDir = Directory.CreateDirectory(pathDir + @"\BufferFolder");
                string BufferFolder = bufferDir.FullName;
                string[] _folders = Directory.GetDirectories(pathDir);
                foreach (var folder in _folders)
                {
                    try
                    {
                        Directory.Move(folder, folder.Replace(pathDir, BufferFolder));
                    }
                    catch (IOException)
                    {

                    }

                }

                return BufferFolder;
            }
            return null;

        }

        public void ZipDirectories()
        {
            if (CheckForDirectories() != null)
            {
                var s = CheckForDirectories();
                try
                {
                    ZipFile.CreateFromDirectory(s, pathDir + @"\BuffZip.rar", CompressionLevel.NoCompression, false);
                    if (Directory.Exists(pathDir + @"\BufferFolder"))
                        Directory.Delete(pathDir + @"\BufferFolder", true);
                }
                catch (UnauthorizedAccessException)
                {

                }

            }
        }

        public void ExtractDirectories()
        {
            try
            {
                ZipFile.ExtractToDirectory(pathDir + @"\BuffZip.rar", pathDir);
                File.Delete(pathDir + @"\BuffZip.rar");
            }
            catch (UnauthorizedAccessException)
            {


            }

        }

    }
}
