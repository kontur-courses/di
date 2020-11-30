using System.Collections.Generic;

namespace TagCloud
{
    public interface IWordsProvider
    {
        IEnumerable<string> GetWords();
    }
}