using System.Collections.Generic;

namespace TagsCloudContainer.WordsFrequencyAnalyzer
{
    public interface IWordsFrequencyAnalyzer
    {
        public Dictionary<string, int> GetWordsFrequency(IEnumerable<string> words);
    }
}