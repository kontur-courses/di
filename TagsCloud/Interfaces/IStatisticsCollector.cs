using System.Collections.Generic;

namespace TagsCloudVisualization.Interfaces
{
    public interface IStatisticsMaker
    {
        IEnumerable<FrequentedWord> GetStatistics(IEnumerable<string> words);
    }
}