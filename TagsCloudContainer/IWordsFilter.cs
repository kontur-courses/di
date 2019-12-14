using System.Collections;
using System.Collections.Generic;

namespace TagsCloudContainer
{
    public interface IWordsFilter
    {
        IEnumerable<string> FilterWords(IEnumerable<string> words);
    }
}