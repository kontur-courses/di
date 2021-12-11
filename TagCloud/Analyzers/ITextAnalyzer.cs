using System.Collections.Generic;

namespace TagCloud.Analyzers
{
    public interface ITextAnalyzer
    {
        Dictionary<string, int> Analyze(IEnumerable<string> words);
    }
}
