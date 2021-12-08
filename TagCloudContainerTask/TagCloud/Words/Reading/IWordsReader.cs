using System.Collections.Generic;

namespace TagCloud.Words.Reading
{
    public interface IWordsReader
    {
        IEnumerable<string> ReadFromFile(string pathToFile);
        IEnumerable<string> ReadFromConsole();
    }
}