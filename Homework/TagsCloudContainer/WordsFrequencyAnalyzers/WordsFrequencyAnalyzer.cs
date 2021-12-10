using System.Collections.Generic;
using System.Linq;

namespace TagsCloudContainer.WordsFrequencyAnalyzers
{
    public class WordsFrequencyAnalyzer : IWordsFrequencyAnalyzer
    {
        public Dictionary<string, int> GetWordsFrequency(IEnumerable<string> words)
        {
            return words.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());
        }
    }
}