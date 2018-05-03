using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace Hash
{
    public class HashComparer : IContainHashFile, ICompareHashes
    {
        public string HashFileName { get; set; }
        private List<string> UnknownHashList;

        public HashComparer(IContainHashFile chf)
        {
            HashFileName = chf.HashFileName;            
        }

        public List<string> CheckHash(string path)
        {
            List<string> SameHashList = new List<string>();
            UnknownHashList = new List<string>();

            foreach (var file in Directory.GetFiles(path).Where(name => !name.EndsWith(HashFileName)))
            {
                IHash md5 = new Md5(file);
                string encFileHash = md5.BeginHashing();
                if (CheckDecriptingFileHash(encFileHash, path))
                {
                    SameHashList.Add(file);

                }
                else
                {
                    UnknownHashList.Add(file);
                }
            }
            return SameHashList;
        }

        public List<string> ReturnUnknowHashes()
        {
            if(UnknownHashList!=null)
            {
                return UnknownHashList;
            }
            return null;
        }

        private bool CheckDecriptingFileHash(string file, string path)
        {
            using (StreamReader reader = new StreamReader(path + HashFileName))
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
    }
}

