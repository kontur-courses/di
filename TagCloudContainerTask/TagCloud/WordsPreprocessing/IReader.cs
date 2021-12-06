using System.Collections.Generic;

namespace TagCloud.WordsPreprocessing
{
    public interface IReader
    {
        IEnumerable<string> ReadWordsFromFile(string pathToFile);
    }
}