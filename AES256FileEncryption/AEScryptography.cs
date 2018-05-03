using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


namespace AES256FileEncryption
{

    class AEScryptography
    {
        public int keySize;
        public byte[] keybytes;
        public event EventHandler<PasswordArgs> PasswordHandler;


        public AEScryptography(int keySize)
        {
            this.keySize = keySize;
        }
        

        public byte[] Encrypt(byte[] plainBytes, string key)
        {
            if (keybytes == null)
                keybytes = SetKeyBytes(key);

            using (RijndaelManaged symmetricKey = new RijndaelManaged())
            {
                symmetricKey.GenerateIV();
                symmetricKey.Mode = CipherMode.CBC;
                symmetricKey.KeySize = keySize;

                using (ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keybytes, symmetricKey.IV))
                using (MemoryStream memoryStream = new MemoryStream())
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(plainBytes, 0, plainBytes.Length);
                    cryptoStream.FlushFinalBlock();
                    return Combine(memoryStream.ToArray(), symmetricKey.IV);
                }
            }
        }
//        public Task<byte[]> WriteObjectDataAsync()
//        {
//            return Task.Factory.StartNew((() => Encrypt()));
//        }

        public byte[] Decrypt(byte[] plainBytes)
        {
            using (RijndaelManaged symmetricKey = new RijndaelManaged())
            {
                symmetricKey.KeySize = keySize;

                using (ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keybytes, SubstractIVFromData(plainBytes)))
                using (MemoryStream memoryStream = new MemoryStream(SubstractDataFromIv(plainBytes)))
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                {
                    byte[] cipherText = new byte[plainBytes.Length];
                    cryptoStream.Read(cipherText, 0, cipherText.Length);
                    return cipherText;
                }
            }
        }

        private byte[] SetKeyBytes(string passPhrase)
        {
            using (Rfc2898DeriveBytes genBytes = new Rfc2898DeriveBytes(passPhrase, 8))
            {
                byte[] keyBytes = genBytes.GetBytes(keySize / 8);
                OnPasswordGenerated(Encoding.Unicode.GetString(keyBytes));
                return keyBytes;
            }
        }

        private byte[] Combine(byte[] data, byte[] IV)
        {
            byte[] combinedDataIv = new byte[data.Length + IV.Length];
            Buffer.BlockCopy(data, 0, combinedDataIv, 0, data.Length);
            Buffer.BlockCopy(IV, 0, combinedDataIv, data.Length, IV.Length);
            return combinedDataIv;
        }

        private byte[] SubstractIVFromData(byte[] DataAndIV)
        {
            int a = DataAndIV.Length - 16;
            byte[] IV;
            Buffer.BlockCopy(DataAndIV, a, IV = new byte[16], 0, IV.Length);
            return IV;
        }

        private byte[] SubstractDataFromIv(byte[] DataAndIv)
        {
            int a = DataAndIv.Length - 16;
            byte[] Data;
            Buffer.BlockCopy(DataAndIv, 0, Data = new byte[a], 0, Data.Length);
            return Data;
        }

        protected virtual void OnPasswordGenerated(string pass)
        {
            PasswordHandler?.Invoke(this, new PasswordArgs() { password = pass });
        }
    }
}