using System.Collections.Generic;

namespace TagsCloudVisualization.Statistics
{
    public interface IWordsStatistics
    {
        public void AddWords(IEnumerable<string> word);
        IEnumerable<WordCount> GetStatistics(int topWordCount = -1);
    }
}