using System.Collections.Generic;

namespace TagsCloudContainer.WordsPreparator
{
    public interface IWordsConverter
    {
        public IEnumerable<WordInfo> Convert(IEnumerable<string> words);
    }
}