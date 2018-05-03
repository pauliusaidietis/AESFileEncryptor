using System;
using System.IO;
using System.IO.Compression;

namespace FileCompressor 
{
   public class DirectoryDecompressor : DirectoryInfo, IDecompress
    {
        public DirectoryDecompressor(string path) : base (path) {}

        public void ExtractDirectories()
        {
            try
            {
                ZipFile.ExtractToDirectory(Path + CombineNameAndExtension(), Path);
                File.Delete(Path + CombineNameAndExtension());
            }
            catch (Exception)
            {
                throw;
            }
        }
               
    }
}
