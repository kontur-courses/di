using System.Collections.Generic;
using System.IO;

namespace TagCloud.Reader
{
    public class TxtWordsFileReader : IWordsFileReader
    {
        public IEnumerable<string> Read(string fileName)
        {
            return File.ReadAllLines(fileName);
        }
    }
}