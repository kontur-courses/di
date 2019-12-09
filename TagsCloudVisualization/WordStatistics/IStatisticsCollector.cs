using System.Collections.Generic;
using TagsCloudVisualization.Core;

namespace TagsCloudVisualization.WordStatistics
{
    public interface IStatisticsCollector
    {
        IEnumerable<(WordStatistics word, int value)> GetStatistics(IEnumerable<Word> words);
    }
}