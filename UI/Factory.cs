using System;
using System.Threading;
using Encryption.Contracts;
using Encryption;
using Hash;
using FileCompressor;


namespace UI
{
    class Factory
    {
        public static CancellationTokenSource CreateCancelationToken()
        {
            return new CancellationTokenSource();
        }

        public static Progress<T> CreateProgress<T>(Action<T> action)
        {
            return new Progress<T>(action);
        }  

        public static IEncrypt CreateEncryptor(IPassword password)
        {
            return new Encrypting(password);
        }

        public static IDecrypt CreateDecryptor (IPassword password)
        {
            return new Decrypting(password);
        }

        public static IPassword CreatePassword(string phrase, int keySize)
        {
            return new Password(phrase, keySize);
        }

        public static IHash CreateHasher(string path)
        {
            return new Md5(path);
        }

        public static DirectoryInfo CreateDirectoryManager(string path)
        {
            return new DirectoryManager(path);
        }

        public static ICompress CreateZipper(string path)
        {
            return new DirectoryCompressor(path, new DirectoryManager(path));
        }

        public static IDecompress CreateUnzipper(string path)
        {
            return new DirectoryDecompressor(path);
        }

        public static ICreateHashFile CreateHashFile(string path)
        {
            return new HashFileCreator(path);
        }
        public static IHash Createhasher(string path)
        {            
                return new Md5(path);            
        }

        public static IContainHashFile CreateHashComparer(IContainHashFile chf)
        {
            return new HashComparer(chf);
        }

    }
}
