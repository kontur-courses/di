using System.Collections.Generic;
using System.IO;

namespace TagsCloud.FileReaders
{
    public class TxtFileReader : IFileReader
    {
        public IEnumerable<string> GetWordsFromFile(string filePath)
        {
            // TODO делать что-то, если больше одного слова в строке.
            return File.ReadAllText(filePath).Split();
        }
    }
}
