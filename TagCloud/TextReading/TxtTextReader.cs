using System.Collections.Generic;
using System.IO;

namespace TagCloud.TextReading
{
    public class TxtTextReader : ITextReader
    {
        private readonly string path;

        public TxtTextReader(string filePath)
        {
            path = filePath;
        }

        public IEnumerable<string> ReadWords()
        {
            return File.ReadLines(path);
        }
    }
}
