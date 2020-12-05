using System.Collections.Generic;
using System.IO;

namespace TagsCloud.FileReaders
{
    public class TxtFileReader : IFileReader
    {
        public IEnumerable<string> GetWordsFromFile(string filePath)
        {
            return File.ReadLines(filePath);
        }
    }
}
