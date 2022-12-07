using System.Collections.Generic;

namespace TagCloud
{
    public interface IWordsFrequencyAnalyzer
    {
        IReadOnlyDictionary<string, double> GetFrequencies(IEnumerable<string> words);
    }
}
