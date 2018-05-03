namespace Hash
{
    public interface ICreateHashFile
    {
       void CreateHashFile(byte[] bytes);
       void TransformStringAndWrite(string fileName, string fileHash);
       void TransfromBase64StringAndWrite(string fileName, string fileHash);
    }
}