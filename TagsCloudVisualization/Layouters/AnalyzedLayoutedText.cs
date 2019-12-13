using System.Collections.Generic;
using TagsCloudVisualization.Core;
using TagsCloudVisualization.WordStatistics;

namespace TagsCloudVisualization.Layouters
{
    public class AnalyzedLayoutedText
    {
        private readonly Dictionary<WordStatistics.WordStatistics, int> statistics;
        public readonly LayoutedWord[] Words;

        public AnalyzedLayoutedText(LayoutedWord[] words, Dictionary<WordStatistics.WordStatistics, int> statistics)
        {
            Words = words;
            this.statistics = statistics;
        }

        public int GetStat(LayoutedWord word, StatisticsType type)
        {
            return statistics[new WordStatistics.WordStatistics(word, type)];
        }
    }
}