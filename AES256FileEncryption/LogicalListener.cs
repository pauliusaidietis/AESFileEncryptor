using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace AES256FileEncryption
{
    public enum Tasks {
        Zip, Encrypt, Decrypt, Unzip
    }
    //    public class HashArgs : EventArgs
    //    {
    //        public string fileName { get; set; }
    //    }

    public class PasswordArgs : EventArgs
    {
        public string password { get; set; }
    }

    class LogicalListener
    {
        // public EventHandler<HashArgs> HashHandler;
        byte[] encrypted;
        private AEScryptography cryptography;
        private HashStoringFile hashAndStore;

        public LogicalListener(AEScryptography aes)
        {
            cryptography = aes;
        }

        public async Task EncryptBytesAsync(IProgress<int> progress, CancellationToken ct, string task, string path, params string[] files)
        {
            await Task.Run(async () =>
            {
                int tempCount = 0;
                if (task != "Zip")
                {
                    int totalCount = files.Length;
                    foreach (var file in files)
                    {
                        byte[] binaryData = File.ReadAllBytes(file);
                        if (task == "Encrypt")
                            await ReadObjectDataAsync(file, task, null, binaryData);
                        if (task == "Decrypt")
                        {
                            await ReadObjectDataAsync(file, task, path, binaryData);
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
                    await ReadObjectDataAsync(path, task);
                    ct.ThrowIfCancellationRequested();
                }

            }, ct);
           
        }
        public Task ReadObjectDataAsync(string fileName, string task, string directory = null, params byte[] fileBytes)
        {
            switch (task)
            {
                case "Zip":
                    return Task.Factory.StartNew(() => OnDirectoryChosen(fileName));
                case "Encrypt":
                    return Task.Factory.StartNew(() => WriteEncryptedBytes(fileName, fileBytes));
                case "Decrypt":
                    return Task.Factory.StartNew(() => DecryptAndWriteBytes(directory, fileName, fileBytes));
                default:
                    return null;
            }

        }

        public void OnDirectoryChosen(string dirPath)
        {
            Zipper zip = new Zipper(dirPath);
            zip.ZipDirectories();
            hashAndStore = new HashStoringFile(dirPath);
        }

        private void WriteEncryptedBytes(string fileName, byte[] fileBytes)
        {
            encrypted = cryptography.Encrypt(fileBytes, "raktas");
            File.WriteAllBytes(fileName, encrypted);
            hashAndStore.OnFileEncrypted(fileName);
        }

        private void DecryptAndWriteBytes(string dirPath, string fileName, byte[] fileBytes)
        {
            byte[] decrypted = cryptography.Decrypt(fileBytes);
            File.WriteAllBytes(dirPath + @"\" + Path.GetFileName(fileName), decrypted);

        }


        //        protected virtual void OnFileEncrypted(string file)
        //        {
        //            MessageBox.Show(":sss");
        //            HashHandler?.Invoke(this, new HashArgs { fileName = file });
        //        }


    }
}
