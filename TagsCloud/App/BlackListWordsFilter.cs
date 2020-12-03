using System.Collections.Generic;
using System.Linq;

namespace TagsCloud.App
{
    public class BlackListWordsFilter : IWordsFilter
    {
        private readonly HashSet<string> blackList;
        public BlackListWordsFilter(HashSet<string> excludedWords, IWordNormalizer normalizer)
        {
            blackList = excludedWords.Select(normalizer.NormalizeWord).ToHashSet();
        }

        public IEnumerable<string> FilterWords(IEnumerable<string> words)
        {
            return words.Where(word => !blackList.Contains(word));
        }
    }
}
