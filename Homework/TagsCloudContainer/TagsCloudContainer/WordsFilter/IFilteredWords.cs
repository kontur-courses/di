using System.Collections.Generic;

namespace TagsCloudContainer.WordsFilter
{
    public interface IFilteredWords
    {
        Dictionary<string, int> FilteredWordsList { get; }
    }
}
