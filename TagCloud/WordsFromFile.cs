using System.Collections.Generic;
using System.IO;

namespace TagsCloud
{
    public class WordsFromFile : IWordCollection
    {
        private readonly string path;

        public WordsFromFile(string path)
        {
            this.path = path;
        }

        public IEnumerable<string> GetWords()
        {
            return File.ReadAllLines(path);
        }
    }
}