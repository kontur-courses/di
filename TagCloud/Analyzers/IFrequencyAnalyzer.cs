using System.Collections.Generic;

namespace TagCloud.Analyzers
{
    public interface IFrequencyAnalyzer
    {
        Dictionary<string, int> Analyze(IEnumerable<string> words);
    }
}
