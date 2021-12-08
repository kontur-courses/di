using System.Collections.Generic;
using System.Text;

namespace TagCloud.Words.Reading
{
    public interface IWordsReader
    {
        IEnumerable<string> ReadFromFile(string pathToFile, Encoding encoding);
        IEnumerable<string> ReadFromConsole();
    }
}