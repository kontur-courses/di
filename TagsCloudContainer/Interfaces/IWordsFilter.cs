using System.Collections.Generic;

namespace TagsCloudContainer.Interfaces
{
    public interface IWordsFilter
    {
        IEnumerable<string> FilterWords(IEnumerable<string> words);
    }
}