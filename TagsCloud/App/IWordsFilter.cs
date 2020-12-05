using System.Collections.Generic;

namespace TagsCloud.App
{
    public interface IWordsFilter
    {
        IEnumerable<string> FilterWords(IEnumerable<string> words);
    }
}