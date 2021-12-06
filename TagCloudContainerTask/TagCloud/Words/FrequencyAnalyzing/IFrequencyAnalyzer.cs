using System.Collections.Generic;

namespace TagCloud.Words.FrequencyAnalyzing
{
    public interface IFrequencyAnalyzer
    {
        Dictionary<string, double> AnalyzeWordsFrequency(IEnumerable<string> words);
    }
}