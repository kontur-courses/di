using System.Collections.Generic;
using TagCloud.Data;

namespace TagCloud.Counter
{
    public interface IWordsCounter
    {
        IEnumerable<WordInfo> GetWordsInfo(IEnumerable<string> words);
    }
}