using System.Collections.Generic;

namespace TagsCloud.Interfaces
{
    public interface IWordCounter
    {
        IEnumerable<(string word, int frequency)> getAllStatistics(IEnumerable<string> words);
    }
}
