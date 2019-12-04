using System.Collections.Generic;
using System.IO;

namespace TagsCloudContainer.Data.Readers
{
    public class WordListReader : IWordReader
    {
        private readonly string path;

        public WordListReader(string path)
        {
            this.path = path;
        }

        public IEnumerable<string> ReadAllWords()
        {
            return File.ReadAllLines(path);
        }
    }
}