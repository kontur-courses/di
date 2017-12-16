using System.Collections.Generic;

namespace TagCloud.Interfaces
{
    public interface IMystemShell
    {
        Result<IEnumerable<string>> GetInterestingWords(string filePath);
    }
}