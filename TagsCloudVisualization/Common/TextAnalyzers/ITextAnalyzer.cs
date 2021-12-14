using System.Collections.Generic;

namespace TagsCloudVisualization.Common.TextAnalyzers
{
    public interface ITextAnalyzer
    {
        public List<WordStatistic> GetWordStatistics(string text);

        public List<WordStatistic> GetWordStatistics(IEnumerable<string> words);
    }
}