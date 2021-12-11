using System.Collections.Generic;

namespace TagsCloudContainer.WordsConverters
{
    public interface IWordsConverter
    {
        public ICollection<WordInfo> Convert(IEnumerable<string> words);
    }
}