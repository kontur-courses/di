using System.Collections.Generic;

namespace TagsCloudContainer.WordProcessing
{
    public interface IWordStatisticsCalculator
    {
        IEnumerable<WordData> CalculateStatistics(IEnumerable<string> words);
    }
}