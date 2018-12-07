using System.Collections.Generic;
using TagCloud.Data;

namespace TagCloud.Counter
{
    public interface IWordsCounter
    {
        IEnumerable<WordInfo> Count(IEnumerable<string> words);
    }
}