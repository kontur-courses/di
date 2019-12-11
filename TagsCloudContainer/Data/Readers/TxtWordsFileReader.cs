using System.Collections.Generic;
using System.IO;

namespace TagsCloudContainer.Data.Readers
{
    public class TxtWordsFileReader : IWordsFileReader
    {
        public IEnumerable<string> ReadAllWords(string path) => File.ReadAllLines(path);
    }
}