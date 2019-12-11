using System.Collections.Generic;

namespace TextConfiguration
{
    public interface IWordsProvider
    {
        List<string> ReadWordsFromFile(string filePath);
    }
}
