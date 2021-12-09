using System.Collections.Generic;

namespace TagCloud.Words.FrequencyAnalyzers
{
    public interface IFrequencyAnalyzer
    {
        Dictionary<string, double> AnalyzeWordsFrequency(IEnumerable<string> words);
    }
}