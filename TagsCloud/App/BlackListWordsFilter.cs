using System.Collections.Generic;
using System.Linq;

namespace TagsCloud.App
{
    public class BlackListWordsFilter : IWordsFilter
    {
        public string[] ExcludedWords => blackList.ToArray();
        private readonly HashSet<string> blackList;
        private readonly IWordNormalizer normalizer;

        public BlackListWordsFilter(HashSet<string> excludedWords, IWordNormalizer normalizer)
        {
            blackList = excludedWords.Select(normalizer.NormalizeWord).ToHashSet();
            this.normalizer = normalizer;
        }

        public IEnumerable<string> FilterWords(IEnumerable<string> words)
        {
            return words.Where(word => !blackList.Contains(word));
        }

        public void UpdateBlackList(IEnumerable<string> words)
        {
            blackList.Clear();
            foreach (var word in words)
                blackList.Add(normalizer.NormalizeWord(word));
        }
    }
}
