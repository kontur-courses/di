using System.Collections.Generic;
using System.Linq;
using TagsCloudTextProcessing.Formatters;

namespace TagsCloudTextProcessing.Filters
{
    public class ExcludeFromListFilter : IWordsFilter
    {
        private readonly HashSet<string> wordsToExcludeHashSet;

        public ExcludeFromListFilter(IEnumerable<string> wordsToExclude)
        {
            wordsToExclude = ConvertToUnifiedFormat(wordsToExclude);
            wordsToExcludeHashSet = new HashSet<string>(wordsToExclude);
        }

        private static IEnumerable<string> ConvertToUnifiedFormat(IEnumerable<string> wordsToExclude)
        {
            wordsToExclude = new LowercaseFormatter().Format(wordsToExclude);
            return wordsToExclude;
        }

        public IEnumerable<string> Filter(IEnumerable<string> inputWords)
        {
            inputWords = ConvertToUnifiedFormat(inputWords);
            return inputWords.Where(w => !wordsToExcludeHashSet.Contains(w));
        }
    }
}