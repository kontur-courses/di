using System.Collections.Generic;

namespace TagCloud
{
    public interface IWordsFilter
    {
        IEnumerable<string> Apply(IEnumerable<string> word);
    }
}