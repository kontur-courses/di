using System.Collections.Generic;
using TagsCloudContainer.WordsPreparator;

namespace TagsCloudContainer.WordsFilter
{
    public interface IWordsFilter
    {
        public IEnumerable<string> Filter(IEnumerable<WordInfo> words);
    }
}