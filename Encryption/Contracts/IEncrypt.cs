namespace Encryption.Contracts
{
    public interface IEncrypt
    {
        byte[] Encrypt(byte[] plainBytes);
    }
}
