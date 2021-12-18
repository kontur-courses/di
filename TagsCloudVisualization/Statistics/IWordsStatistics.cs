using System.Collections.Generic;
using ResultProject;

namespace TagsCloudVisualization.Statistics
{
    public interface IWordsStatistics
    {
        public void AddWords(IEnumerable<string> word);
        Result<IEnumerable<WordCount>> GetStatistics(uint topWordCount);
        Result<IEnumerable<WordCount>> GetStatistics();
    }
}