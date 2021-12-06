using System.Collections.Generic;

namespace TagCloud.TextHandlers.Filters
{
    public interface ITextFilter
    {
        IEnumerable<string> Filter(IEnumerable<string> words);
    }
}