using System.Collections.Generic;

namespace TagsCloudContainer.WordsPreparator
{
    public interface IWordsPreparator
    {
        public IEnumerable<WordInfo> Prepare(IEnumerable<string> words);
    }
}