using System.Collections.Generic;
using TagCloud.Infrastructure;

namespace TagCloud
{
    public interface IWordsProvider
    {
        IEnumerable<Word> GetWords();
    }
}
