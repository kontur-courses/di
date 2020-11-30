using System.Collections.Generic;
using System.IO;

namespace TagCloud.Core.FileReaders
{
    public class TxtReader : IFileReader
    {
        public string Extension => ".txt";

        public IEnumerable<string> ReadAllWords(string filePath)
        {
            return File.ReadAllText(filePath).Split();
        }
    }
}