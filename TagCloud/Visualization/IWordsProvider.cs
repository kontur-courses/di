using System.Collections.Generic;
using TagCloud.Infrastructure;

namespace TagCloud.Visualization
{
    public interface IWordsProvider
    {
        IEnumerable<Word> GetWords();
    }
}
