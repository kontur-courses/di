using System.Collections.Generic;

namespace TagsCloudVisualization.WordsProcessing
{
    public interface IWordsProvider
    {
        IEnumerable<string> GetWords();
    }
}