using System.Collections.Generic;

namespace TagCloud.Analyzers
{
    public interface IWordFilter
    {
        IEnumerable<string> Analyze(IEnumerable<string> words);
    }
}
