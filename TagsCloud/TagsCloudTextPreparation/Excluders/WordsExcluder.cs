using System.Collections.Generic;
using System.Linq;

namespace TagsCloudTextPreparation.Excluders
{
    public class WordsExcluder : IWordsExcluder
    {
        public IEnumerable<string> ExcludeWords(IEnumerable<string> inputWords, IEnumerable<string> wordsToExclude)
        {
            var wordsToExcludeHashSet = new HashSet<string>(wordsToExclude);
            return inputWords.Where(w => !wordsToExcludeHashSet.Contains(w));
        }
    }
}