using System.Collections.Generic;
using TagsCloudVisualization.Core;
using TagsCloudVisualization.Layouters;

namespace TagsCloudVisualization.WordStatistics
{
    public class AnalyzedText
    {
        private readonly Dictionary<WordStatistics, int> statistics;
        public readonly Word[] Words;

        public AnalyzedText(Word[] words, Dictionary<WordStatistics, int> statistics)
        {
            Words = words;
            this.statistics = statistics;
        }

        public int GetStat(Word word, StatisticsType type)
        {
            return statistics[new WordStatistics(word, type)];
        }

        public AnalyzedLayoutedText ToLayoutedText(LayoutedWord[] layoutedWords)
        {
            return new AnalyzedLayoutedText(layoutedWords, statistics);
        }
    }
}