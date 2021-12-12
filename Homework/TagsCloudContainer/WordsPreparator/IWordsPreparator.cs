using System.Collections.Generic;

namespace TagsCloudContainer.WordsPreparator
{
    public interface IWordsPreparator
    {
        public ICollection<WordInfo> Prepare(IEnumerable<string> words);
    }
}