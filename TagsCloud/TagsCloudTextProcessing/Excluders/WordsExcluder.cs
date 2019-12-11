using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TagsCloudTextProcessing.Excluders
{
    public class WordsExcluder : IWordsExcluder
    {
        public WordsExcluder()
        {
        }
        public IEnumerable<string> ExcludeWords(IEnumerable<string> inputWords, IEnumerable<string> wordsToExclude)
        {
            var wordsToExcludeHashSet = new HashSet<string>(wordsToExclude);
            return inputWords.Where(w => !wordsToExcludeHashSet.Contains(w));
        }
    }
}