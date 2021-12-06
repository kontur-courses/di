using System.Collections.Generic;

namespace TagCloud.WordsReading
{
    public interface IReader
    {
        IEnumerable<string> ReadWordsFromFile(string pathToFile);
    }
}