using System.Collections.Generic;

namespace TagCloud.TextAnalyzer
{
    public interface ITextAnalyzer
    {
        public Dictionary<string, int> GetWordCounts(List<string> words);
    }
}