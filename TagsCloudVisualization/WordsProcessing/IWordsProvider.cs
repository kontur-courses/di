using System.Collections.Generic;

namespace TagsCloudVisualization
{
    public interface IWordsProvider
    {
        IEnumerable<string> GetWords();
    }
}