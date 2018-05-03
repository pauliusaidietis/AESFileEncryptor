using System.IO;


namespace FileCompressor
{
    public class DirectoryManager : DirectoryInfo
    {
        public DirectoryManager(string path) : base(path) { }
        
        private int CountDirectories()
        {
            string[] directoryCount = Directory.GetDirectories(Path);
            int dirNumber = directoryCount.Length;
            return dirNumber;
        }

        public string MoveToCommonDir()
        {            
                string bufferFolder = MakeDirectory();
                string[] _folders = Directory.GetDirectories(Path);
                foreach (var folder in _folders)
                {
                    try
                    {
                        Directory.Move(folder, folder.Replace(Path, bufferFolder));
                    }
                    catch (IOException)
                    {
                    
                    }

                }

                return bufferFolder;           
         
        }

        public bool CheckIfDirExists ()
        {
            if (CountDirectories() >= 1)
                return true;
            else
                return false;
        }

        public string MakeDirectory()
        {
            var bufferDir = Directory.CreateDirectory(Path + CommonDirName);
            string bufferFolderPath = bufferDir.FullName;
            return bufferFolderPath;
        }
    }
}
