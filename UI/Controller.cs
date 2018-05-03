using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Encryption;
using Encryption.Contracts;
using FileCompressor;
using Hash;

namespace UI
{
    public enum Tasks
    {
        Zip, Encrypt, Decrypt, Unzip
    }

    public class PasswordArgs : EventArgs
    {
        public string Password { get; set; }
    }

    class Controller
    {       
        private IEncrypt Encryptor { get; set; }
        private IDecrypt Decryptor { get; set; }
        private ICreateHashFile HashFile { get; set;}
        private IPassword Password { get; set; }
        private Controller _controller {get; set; }
        private CancellationTokenSource _cts { get; set; }
        private IHash Md5 { get; set; }

        public Controller()
        {
        }

        public Controller(string path)
        {
            if (HashFile == null)
                HashFile = Factory.CreateHashFile(path);            
        }
        

        public async Task CryptographyTask(IProgress<int> progress, CancellationToken ct, Tasks task, string path, params string[] files)
        {            
            await Task.Run( async () =>
            {
                int totalCount = 0;
                int tempCount = 0;
                if (task != Tasks.Zip)
                {
                    if(task == Tasks.Encrypt)
                    {
                        totalCount = files.Length-1;
                    }
                    else
                    {
                        totalCount = files.Length;
                    }                   
                    

                    foreach (var file in files.Where(name => !name.EndsWith("HashValues.txt")))
                    {
                        byte[] binaryData = File.ReadAllBytes(file);
                        if (task == Tasks.Encrypt)
                        {
                            await ReturnCryptographyTask(file, task, null, binaryData);
                        }
                        if (task == Tasks.Decrypt)
                        {                            
                            await ReturnCryptographyTask(file, task, path, binaryData);
                        }

                        ct.ThrowIfCancellationRequested();

                        if (progress != null)
                        {
                            progress.Report(tempCount * 100 / totalCount);
                            Thread.Sleep(200);
                        }

                        tempCount++;
                    }

                    progress.Report(tempCount * 100 / totalCount);
                }
                else
                {
                    if (progress != null)
                    {
                        progress.Report(99);
                    }                   
                    
                    await ReturnCryptographyTask(path, task);
                    ct.ThrowIfCancellationRequested();
                }

            }, ct);
        }
        public Task ReturnCryptographyTask(string fileName, Tasks task, string directory = null, params byte[] fileBytes)
        {
            switch (task)
            {
                case Tasks.Zip:
                    return Task.Factory.StartNew(() => CompressAndWritePassWordHash(fileName));
                case Tasks.Encrypt:
                    return Task.Factory.StartNew(() => WriteEncryptedBytes(fileName, fileBytes));
                case Tasks.Decrypt:
                    return Task.Factory.StartNew(() => DecryptAndWriteBytes(directory, fileName, fileBytes));
                default:
                    return null;
            }
        }

        public void CompressAndWritePassWordHash(string dirPath)
        {             
            ICompress zip = CreateZipper(dirPath);
            zip.ZipDirectories();
            HashFile = Factory.CreateHashFile(dirPath);            
            HashFile.TransfromBase64StringAndWrite(Password.Salt, Password.Hash);
            Md5 = CreateHasher(Path.GetFullPath(dirPath));            
        }

        private void WriteEncryptedBytes(string fileName, byte[] fileBytes)
        {
            Encryptor = Factory.CreateEncryptor(Password);
            byte[] Encrypted = Encryptor.Encrypt(fileBytes);
            File.WriteAllBytes(fileName, Encrypted);
            Md5 = CreateHasher(fileName);
            string hashString = Md5.BeginHashing();
            HashFile.TransformStringAndWrite(fileName, hashString);
        }

        private void DecryptAndWriteBytes(string dirPath, string fileName, byte[] fileBytes)
        {
            Decryptor = Factory.CreateDecryptor(Password);
            byte[] decrypted = Decryptor.Decrypt(fileBytes);
            File.WriteAllBytes(dirPath + @"\" + Path.GetFileName(fileName), decrypted);

        }
        
        public bool ValidatePassword(string path)
        {
           string hash =  GetPasswordHashFromFile(path);            
           return Password.ValidatePassword(hash);            
        }

        public string GetPasswordHashFromFile(string path)
        {
            string hash;
            using (StreamReader reader = new StreamReader(path + @"\HashValues.txt"))
            {
                hash = reader.ReadLine();
            }
            return hash;
            
        }

        public void UnzipDirectories(string path)
        {
            IDecompress dec = CreateUnZipper(path);
            dec.ExtractDirectories();
        }

        public ICompress CreateZipper(string dirPath)
        {                    
            return Factory.CreateZipper(dirPath);
        }

        public IDecompress CreateUnZipper(string dirPath)
        {
            return Factory.CreateUnzipper(dirPath);
        }

       
        public IHash CreateHasher(string path)
        {
            return Factory.CreateHasher(path);
        }       

        public IPassword CreatePassword(string phrase, int keySize, string salt=null)
        {           
            try
            {
                Password = Factory.CreatePassword(phrase, keySize);
                if (salt == null)
                {
                    Password.HashPassword();
                }
                else
                {
                    Password.HashPassword(salt);
                }
                
                return Password;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public CancellationTokenSource CreateCancelationToken()
        {
            return Factory.CreateCancelationToken();
        }

        public Progress<T> CreateProgress<T>(Action<T> action)
        {
            return Factory.CreateProgress(action);
        }

        public IContainHashFile CreateHashComparer()
        {
            return Factory.CreateHashComparer((IContainHashFile)HashFile);
        }

        public List<string> CheckHash(string path)
        {            
           ICompareHashes comparer = (ICompareHashes)CreateHashComparer();
            return comparer.CheckHash(path);
        }       

       
    }

}
