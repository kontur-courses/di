using System.Collections.Generic;

namespace TagsCloudTextProcessing.Excluders
{
    public interface IWordsExcluder
    {
        IEnumerable<string> ExcludeWords(IEnumerable<string> inputWords, IEnumerable<string> wordsToExclude);
    }
}