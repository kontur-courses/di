using System.Collections.Generic;
using TagsCloud.Interfaces;

namespace TagsCloud.TextWorkers
{
    public class WordsFrequencyAnalyzer : IWordsFrequencyAnalyzer
    {
        private readonly IComparer<int> comparer;

        public WordsFrequencyAnalyzer(IComparer<int> dictionaryComparer)
        {
            comparer = dictionaryComparer;
        }

        public SortedDictionary<int, List<string>> GetSortedDictOfWordsFreq(IEnumerable<string> normalFormWords)
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

            var orderedWordsByFreq = GetOrderedDictByFrequency(wordsFrequency);

            return orderedWordsByFreq;
        }

        private SortedDictionary<int, List<string>> GetOrderedDictByFrequency(Dictionary<string, int> wordsFrequency)
        {
            var orderedWordsFrequency = new SortedDictionary<int, List<string>>(comparer);
               
            foreach (var pair in wordsFrequency)
            {
                var word = pair.Key;
                var freq = pair.Value;

                if (orderedWordsFrequency.ContainsKey(freq))
                {
                    orderedWordsFrequency[freq].Add(word);
                }
                else
                {
                    orderedWordsFrequency.Add(freq, new List<string>() { word });
                }
            }

            return orderedWordsFrequency;
        }
    }
}
