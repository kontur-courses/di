using System.Collections.Generic;

namespace TagCloud
{
    public interface IWordsProvider
    {
        IEnumerable<WordToken> GetTokens();
    }
}