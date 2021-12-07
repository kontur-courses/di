using System.Collections.Generic;

namespace TagsCloudVisualization
{
    public interface IWordsStatistics
    {
        void AddWord(string word);
        IEnumerable<WordCount> GetStatistics();
    }
}