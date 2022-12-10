using System.Collections.Generic;
using System.Linq;

namespace TagCloud.FrequencyAnalyzer
{
    public class WordsFrequencyAnalyzer : IWordsFrequencyAnalyzer
    {
        public IReadOnlyDictionary<string, double> GetFrequencies(IEnumerable<string> words)
        {
            var wordsCount = words.Count();

            return words
                    .GroupBy(word => word)
                    .OrderByDescending(group => group.Count())
                    .ToDictionary(group => group.Key, group => (double)group.Count() / wordsCount);   
        }
    }
}
