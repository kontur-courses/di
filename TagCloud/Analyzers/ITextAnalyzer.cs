using System.Collections.Generic;

namespace TagCloud.Analyzers
{
    public interface ITextAnalyzer
    {
        IEnumerable<string> Analyze(string[] words);
    }
}
