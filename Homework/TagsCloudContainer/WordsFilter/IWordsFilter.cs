using System.Collections.Generic;
using TagsCloudContainer.WordsPreparator;

namespace TagsCloudContainer.WordsFilter
{
    public interface IWordsFilter
    {
        public FilterType FilterType { get; }
        public ICollection<WordInfo> Filter(ICollection<WordInfo> words);
    }
}