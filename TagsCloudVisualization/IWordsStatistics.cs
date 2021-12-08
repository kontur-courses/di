using System.Collections.Generic;

namespace TagsCloudVisualization
{
    public interface IWordsStatistics
    {
        void Load();
        IEnumerable<WordCount> GetStatistics();
    }
}