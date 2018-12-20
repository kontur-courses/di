using System.Collections.Generic;

namespace TagsCloudContainer.WordsFilter
{
    public interface IWordsFilters
    {
        Dictionary<string, int> FilteredWords { get; }
        void RemoveIgnoredWords();
        void RemoveWordsOutOfLengthRange(int leftBound, int rightBound = int.MaxValue);
    }
}
