using System.Collections.Generic;
using System.Linq;

namespace TagsCloud.App
{
    public class BlackListWordsFilter : IWordsFilter
    {
        internal readonly HashSet<string> BlackList;
        private readonly IWordNormalizer normalizer;

        public BlackListWordsFilter(HashSet<string> excludedWords, IWordNormalizer normalizer)
        {
            BlackList = excludedWords.Select(normalizer.NormalizeWord).ToHashSet();
            this.normalizer = normalizer;
        }

        public IEnumerable<string> FilterWords(IEnumerable<string> words)
        {
            return words.Where(word => !BlackList.Contains(word));
        }

        public void UpdateBlackList(IEnumerable<string> words)
        {
            BlackList.Clear();
            foreach (var word in words)
                BlackList.Add(normalizer.NormalizeWord(word));
        }
    }
}