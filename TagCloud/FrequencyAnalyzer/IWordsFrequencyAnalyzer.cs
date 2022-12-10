using System.Collections.Generic;

namespace TagCloud.FrequencyAnalyzer
{
    public interface IWordsFrequencyAnalyzer
    {
        IReadOnlyDictionary<string, double> GetFrequencies(IEnumerable<string> words);
    }
}
