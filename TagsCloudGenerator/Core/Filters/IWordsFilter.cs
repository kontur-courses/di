using System.Collections.Generic;

namespace TagsCloudGenerator.Core.Filters
{
    public interface IWordsFilter
    {
        IEnumerable<string> GetFilteredWords(IEnumerable<string> words);
    }
}