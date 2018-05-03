using Encryption.Contracts;
using System;
using System.IO;
using System.Security.Cryptography;

namespace Encryption 
{
   public class Encrypting : IEncrypt
    {
       
        private readonly IPassword _password;

        public Encrypting(IPassword password)
        {              
              this._password = password;
        }

        public byte[] Encrypt(byte[] plainBytes)
        {                  
            using (RijndaelManaged symmetricKey = new RijndaelManaged())
            {
                symmetricKey.GenerateIV();
                symmetricKey.Mode = CipherMode.CBC;
                symmetricKey.KeySize = _password.KeySize;
                symmetricKey.Padding = PaddingMode.PKCS7;
                              

                using (ICryptoTransform encryptor = symmetricKey.CreateEncryptor(_password.HashBytes, symmetricKey.IV))
                using (MemoryStream memoryStream = new MemoryStream())
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {                    
                    cryptoStream.Write(plainBytes, 0, plainBytes.Length);
                    cryptoStream.FlushFinalBlock();
                    return Combine(memoryStream.ToArray(), symmetricKey.IV);
                }
            }
        }

        private byte[] Combine(byte[] data, byte[] IV)
        {
            byte[] combinedDataIv = new byte[data.Length + IV.Length];
            Buffer.BlockCopy(data, 0, combinedDataIv, 0, data.Length);
            Buffer.BlockCopy(IV, 0, combinedDataIv, data.Length, IV.Length);
            return combinedDataIv;
        }       

    }
       
}
