using System.Collections.Generic;
using TagCloud.selectors;

namespace TagCloud.repositories
{
    public interface IWordHelper
    {
        IEnumerable<WordStatistic> GetWordStatistics(IEnumerable<string> words);

        IEnumerable<string> FilterWords(IEnumerable<string> words);

        IEnumerable<string> ConvertWords(IEnumerable<string> words);
    }
}