namespace Encryption.Contracts
{
    public interface IDecrypt
    {
        byte[] Decrypt(byte[] plainBytes);
    }
}
