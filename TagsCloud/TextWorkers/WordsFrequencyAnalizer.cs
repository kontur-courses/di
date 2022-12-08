using System.Collections.Generic;
using System.Linq;

namespace TagsCloud.TextWorkers
{
    public static class WordsFrequencyAnalizer
    {
        public static Dictionary<string, int> GetSortedDictOfWordsFreq(IEnumerable<string> normalFormWords)
        {
            var wordsFrequency = new Dictionary<string, int>();

            foreach (var word in normalFormWords)
            {
                if (wordsFrequency.ContainsKey(word))
                {
                    wordsFrequency[word]++;
                }
                else
                {
                    wordsFrequency.Add(word, 1);
                }
            }

            wordsFrequency = GetOrderedDictByFrequency(wordsFrequency);

            return wordsFrequency;
        }

        private static Dictionary<string, int> GetOrderedDictByFrequency(Dictionary<string, int> wordsFrequency)
        {
            wordsFrequency = wordsFrequency
                .OrderByDescending(x => x.Value)
                .ToDictionary(x => x.Key, y => y.Value);

            return wordsFrequency;
        }
    }
}
