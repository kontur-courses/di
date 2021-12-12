using System.Collections.Generic;
using TagsCloudContainer.WordsPreparator;

namespace TagsCloudContainer.WordsFilter
{
    public interface IWordsFilter
    {
        public ICollection<WordInfo> Filter(ICollection<WordInfo> words);
    }
}