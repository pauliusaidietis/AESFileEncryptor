using System;
using System.IO;
using System.IO.Compression;


namespace FileCompressor
{
   public class DirectoryCompressor : DirectoryInfo, ICompress
    {
        public DirectoryManager dir;       

        public DirectoryCompressor(string path, DirectoryManager DirObj) : base (path)
        {
            dir = DirObj;            
        }
        public void ZipDirectories()
        {
            if (dir.CheckIfDirExists() != false)
            {                
                string sourceDirectory = dir.MakeDirectory();
                dir.MoveToCommonDir();
                try
                {                    
                    ZipFile.CreateFromDirectory(sourceDirectory, CombinePathAndCommonDir(), CompressionLevel.NoCompression, false);
                    if (Directory.Exists(Path + CommonDirName))
                        Directory.Delete(Path + CommonDirName, true);
                }
                catch (Exception)
                {
                    throw;
                }

            }
        }
    }
}
