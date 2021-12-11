using System.Collections.Generic;

namespace App.Infrastructure.Words.FrequencyAnalyzers
{
    public interface IFrequencyAnalyzer
    {
        Dictionary<string, double> AnalyzeWordsFrequency(IEnumerable<string> words);
    }
}