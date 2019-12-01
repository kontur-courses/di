using System.Collections.Generic;
using System.Linq;

namespace TagCloudContainer
{
    public static class WordProcessor
    {
        public static IEnumerable<(string word, int occurrenceCount)> CountWordOccurrences(IEnumerable<string> words)
        {
            var wordsCounter = new Dictionary<string, int>();
            //todo skip boring words
            //todo add word filter
            foreach (var word in words)
            {
                var lowerWord = word.ToLower();
                if (!wordsCounter.ContainsKey(lowerWord))
                {
                    wordsCounter[lowerWord] = 0;
                }

                wordsCounter[lowerWord] += 1;
            }

            return wordsCounter.Select(e => (e.Key, e.Value));
        }
    }
}