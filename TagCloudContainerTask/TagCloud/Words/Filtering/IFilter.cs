using System.Collections.Generic;

namespace TagCloud.Words.Filtering
{
    public interface IFilter
    {
        IEnumerable<string> FilterWords(IEnumerable<string> words);
    }
}