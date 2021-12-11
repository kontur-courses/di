using System.Collections.Generic;

namespace TagCloud.Analyzers
{
    public interface IWordConverter
    {
        IEnumerable<string> Convert(IEnumerable<string> words);
    }
}
