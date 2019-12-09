using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TagCloud
{
    public class WordsHandler : IWordsHandler
    {
        public Dictionary<string, int> Conversion(Dictionary<string, int> wordsAndCount)
        {
            return wordsAndCount;
        }

        public Dictionary<string, int> GetWordsAndCount(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException();
            if (path == null)
                throw new ArgumentNullException();
            return File.ReadAllLines(path, Encoding.UTF8)
                .Where(s=>s!= string.Empty)
                .GroupBy(word=>word)
                .ToDictionary(g =>g.Key,g=>g.Count());
        }
    }
}