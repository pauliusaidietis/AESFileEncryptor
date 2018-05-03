using System;
using System.IO;
using System.Security.Cryptography;

using Encryption.Contracts;

namespace Encryption
{
   public class Decrypting : IDecrypt
    {
        private readonly IPassword _password;

        public Decrypting(IPassword password)
        {
            this._password = password;
        }

        public byte[] Decrypt(byte[] plainBytes)
        {          
            using (RijndaelManaged symmetricKey = new RijndaelManaged())
            {
                symmetricKey.KeySize = _password.KeySize;
                symmetricKey.Padding = PaddingMode.PKCS7;

                using (ICryptoTransform decryptor = symmetricKey.CreateDecryptor(_password.HashBytes, SubstractIVFromData(plainBytes)))
                using (MemoryStream memoryStream = new MemoryStream(SubstractDataFromIv(plainBytes)))
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                {                    
                    byte[] cipherText = new byte[plainBytes.Length];
                    cryptoStream.Read(cipherText, 0, cipherText.Length);
                    return cipherText;
                }
            }
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

    }
}
