using System.Collections.Generic;
using TagCloud.IntermediateClasses;

namespace TagCloud.Interfaces
{
    public interface IStatisticsCollector
    {
        IEnumerable<FrequentedWord> GetStatistics(IEnumerable<string> words);
    }
}