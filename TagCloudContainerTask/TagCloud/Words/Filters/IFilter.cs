using System.Collections.Generic;

namespace TagCloud.Words.Filters
{
    public interface IFilter
    {
        IEnumerable<string> FilterWords(IEnumerable<string> words);
    }
}