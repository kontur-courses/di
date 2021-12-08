using System.Collections.Generic;

namespace TagsCloudContainer.WordsFrequencyAnalyzers
{
    public interface IWordsFrequencyAnalyzer
    {
        public Dictionary<string, int> GetWordsFrequency(IEnumerable<string> words);
    }
}