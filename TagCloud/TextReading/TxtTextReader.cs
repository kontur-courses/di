using System.Collections.Generic;
using System.IO;

namespace TagCloud.TextReading
{
    public class TxtTextReader : ITextReader
    {
        public IEnumerable<string> ReadWords(FileInfo file)
        {
            return File.ReadLines(file.FullName);
        }

        public string Extension => ".txt";
    }
}
