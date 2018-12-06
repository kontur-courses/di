using System.Collections.Generic;

namespace TagCloud.Interfaces
{
    public interface IStatisticsCollector
    {
        IEnumerable<FrequentedWord> GetStatistics(IEnumerable<string> words);
    }
}