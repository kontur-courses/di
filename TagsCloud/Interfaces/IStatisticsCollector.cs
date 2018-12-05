using System.Collections.Generic;

namespace TagsCloudVisualization.Interfaces
{
    public interface IStatisticsCollector
    {
        IEnumerable<FrequentedWord> GetStatistics(IEnumerable<string> words);
    }
}