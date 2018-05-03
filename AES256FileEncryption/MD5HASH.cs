using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace AES256FileEncryption
{
    class MD5HASH
    {

        byte[] buffer = new byte[4096];
        int bytesRead;
        private string path;

        public MD5HASH(string path)
        {
            this.path = path;
        }

        public string BeginHashing()
        {
            using (Stream f = File.OpenRead(path))
            using (HashAlgorithm hasher = MD5.Create())
            {                
                do
                {
                    bytesRead = f.Read(buffer, 0, buffer.Length);
                    hasher.TransformBlock(buffer, 0, bytesRead, null, 0);
                } while (bytesRead != 0);

                hasher.TransformFinalBlock(buffer, 0, 0);
                string returned = ConstructHashString(hasher.Hash);
                
                return returned;
            }
        }

        private string ConstructHashString(byte[] hashValue)
        {
            StringBuilder hashBuilder = new StringBuilder(32);

            foreach (byte b in hashValue)
            {
                hashBuilder.Append(b.ToString("x2"));
            }
            return hashBuilder.ToString();
        }
    }
}
