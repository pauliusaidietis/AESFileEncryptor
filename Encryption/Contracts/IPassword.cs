namespace Encryption
{
    public interface IPassword
    {
        int KeySize { get; set; }
        byte[] HashBytes { get; set; }
        string Hash { get; }
        string Salt { get; }

        void HashPassword();
        bool ValidatePassword(string correctHash);
        void HashPassword(string correctHash);
    }
}