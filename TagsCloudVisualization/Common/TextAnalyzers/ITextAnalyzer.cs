using System.Collections.Generic;

namespace TagsCloudVisualization.Common.TextAnalyzers
{
    public interface ITextAnalyzer
    {
        public Dictionary<string, int> GetWordStatistics(string text);

        public Dictionary<string, int> GetWordStatistics(IEnumerable<string> words);
    }
}