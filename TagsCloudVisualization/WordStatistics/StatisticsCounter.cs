using System.Collections.Generic;
using TagsCloudVisualization.Core;

namespace TagsCloudVisualization.WordStatistics
{
    public class StatisticsCounter
    {
        private readonly IStatisticsCollector[] statCollectors;

        public StatisticsCounter(IStatisticsCollector[] collectors)
        {
            statCollectors = collectors;
        }

        public AnalyzedText GetAnalyzedText(Word[] words)
        {
            var statistics = new Dictionary<WordStatistics, int>();
            foreach (var collector in statCollectors)
            foreach (var (wordStatistics, value) in collector.GetStatistics(words))
                statistics[wordStatistics] = value;
            return new AnalyzedText(words, statistics);
        }
    }
}