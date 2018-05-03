namespace FileCompressor
{
    public abstract class DirectoryInfo
    {
        public string CommonDirName { get; set; } = @"\BufferFolder";
        public string Path { get; set; }
        public string Extension { get; set; } = ".rar";

        public DirectoryInfo(string path)
        {
           Path = path;
        }

        protected string CombineNameAndExtension()
        {
            return CommonDirName + Extension;
        }
        protected string CombinePathAndCommonDir()
        {
            return Path + CombineNameAndExtension();
        }
    }
}
