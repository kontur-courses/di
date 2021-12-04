using System.Collections.Generic;

namespace TagsCloudVisualization.WordsProvider
{
    public interface IWordsProvider
    {
        IEnumerable<string> GetWords();
    }
}