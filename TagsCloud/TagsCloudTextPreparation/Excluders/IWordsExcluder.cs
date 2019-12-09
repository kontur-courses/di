using System.Collections.Generic;

namespace TagsCloudTextPreparation.Excluders
{
    public interface IWordsExcluder
    {
        IEnumerable<string> ExcludeWords(IEnumerable<string> inputWords, IEnumerable<string> wordsToExclude);
    }
}