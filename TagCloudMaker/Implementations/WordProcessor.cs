using System.Collections.Generic;
using System.Linq;

namespace TagCloud
{
    public class WordProcessor: IWordProcessor
    {
        private readonly IEnumerable<string> badWords;
        private readonly bool fromLiterature;

        public WordProcessor(IEnumerable<string> badWords, bool fromLiterature)
        {
            this.badWords = badWords;
            this.fromLiterature = fromLiterature;
        }

        public IDictionary<string, int> GetFrequencyDictionary(IEnumerable<string> words)
        {
            return words.Select(s => s.ToLower())
                .Except(badWords)
                .GroupBy(w => w)
                .OrderByDescending(g => g.Count())
                .ToDictionary(g => g.Key, g => g.Count());
        }
    }
}