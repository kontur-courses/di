using System.Collections.Generic;

namespace TagsCloud.Interfaces
{
    public interface IWordCounter
    {
        IEnumerable<(string word, int frequency)> GetWordsStatistics(IEnumerable<string> words);
    }
}
