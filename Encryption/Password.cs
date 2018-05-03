using System;
using System.Security.Cryptography;


namespace Encryption
{
    public class Password : IPassword
    {
       public int KeySize { get; set; }
       public byte[] HashBytes { get; set; }

        public const int SaltByteSize = 24;
        public const int Pbkdf2Iterations = 1000;
        public const int IterationIndex = 0;
        public const int SaltIndex = 0;
        public const int Pbkdf2Index = 1;
        public string Salt { get; private set; }
        public string Hash { get; private set; }
        string PassPhrase { get; set; }

        public Password(string passPhrase, int keySize)
        {
            PassPhrase = passPhrase;
            KeySize = keySize;            
        }
          
        public void HashPassword()
        {           
            var cryptoProvider = new RNGCryptoServiceProvider();
            byte[] salt = new byte[SaltByteSize];
            cryptoProvider.GetBytes(salt);

            HashBytes = GetPbkdf2Bytes(PassPhrase, salt, KeySize/8);
            
            Salt = Convert.ToBase64String(salt);
            Hash = Convert.ToBase64String(HashBytes);
                       
        }

        public void HashPassword(string correctHash)
        {
            char[] delimiter = { ':' };
            var split = correctHash.Split(delimiter);            
            var hash = Convert.FromBase64String(split[Pbkdf2Index]);
            var salt = Convert.FromBase64String(split[SaltIndex]);
            
            HashBytes = GetPbkdf2Bytes(PassPhrase, salt, KeySize / 8);
            Salt = Convert.ToBase64String(salt);
            Hash = Convert.ToBase64String(HashBytes);           

        }

        public bool ValidatePassword(string correctHash)
        {
            char[] delimiter = { ':' };
            var split = correctHash.Split(delimiter);
            var salt = Convert.FromBase64String(split[SaltIndex]);
            var hash = Convert.FromBase64String(split[Pbkdf2Index]);
            var testHash = GetPbkdf2Bytes(PassPhrase, salt,  hash.Length);
            return Convert.ToBase64String(testHash) == split[Pbkdf2Index];
        }

        private byte[] GetPbkdf2Bytes(string password, byte[] salt, int outputBytes)
        {
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt);
            pbkdf2.IterationCount = Pbkdf2Iterations;
            return pbkdf2.GetBytes(outputBytes);
        }
        
    }
   
}
