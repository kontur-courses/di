using System.Collections.Generic;

namespace TagCloud
{
    public abstract class WordsProvider : IWordsProvider
    {
        public abstract IEnumerable<string> GetWords();
    }
}