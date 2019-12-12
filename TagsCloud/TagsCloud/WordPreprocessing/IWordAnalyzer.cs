using System.Collections.Generic;

namespace TagsCloud.WordPreprocessing
{
    public interface IWordAnalyzer
    {
        Dictionary<string, int> GetWordsStatistics(IEnumerable<string> words);
    }
}