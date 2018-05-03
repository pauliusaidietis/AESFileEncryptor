using System.Collections.Generic;

namespace Hash
{
    public interface ICompareHashes
    {
        List<string> CheckHash(string path);
    }
}