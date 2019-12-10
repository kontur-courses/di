using System.Collections.Generic;
using System.IO;

namespace TagCloud.TextReading
{
    public class TxtTextReader : ITextReader
    {
        public IEnumerable<string> ReadWords(string filePath)
        {
            return File.ReadLines(filePath);
        }
    }
}
