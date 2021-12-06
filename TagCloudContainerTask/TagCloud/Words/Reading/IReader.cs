using System.Collections.Generic;

namespace TagCloud.Words.Reading
{
    public interface IReader
    {
        IEnumerable<string> ReadWordsFromFile(string pathToFile);
    }
}