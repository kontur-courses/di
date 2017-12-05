using System.Collections.Generic;
using System.IO;
using System.Linq;
using TagCloud.Interfaces;

namespace TagCloud.Implementations
{
    public class WordProcessor: IWordProcessor
    {
        private readonly IEnumerable<string> badWords;
        private readonly bool fromLiterature;

        public WordProcessor(IEnumerable<string> badWords, bool fromLiterature)
        {
            this.badWords = badWords.Select(w => w.ToLower());
            this.fromLiterature = fromLiterature;
        }

        public IDictionary<string, int> GetFrequencyDictionary(string filePath)
        {
            return File.ReadLines(filePath)
                .Select(s => s.ToLower())
                .Except(badWords)
                .GroupBy(w => w)
                .OrderByDescending(g => g.Count())
                .ToDictionary(g => g.Key, g => g.Count());
        }
    }
}