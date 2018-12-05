using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TagsCloudContainer.WordsReaders
{
    public class TxtReader : IWordsReader
    {
        public IEnumerable<string> GetWords(string filename)
        {
            return File.ReadLines(filename, Encoding.UTF8);
        }
    }
}