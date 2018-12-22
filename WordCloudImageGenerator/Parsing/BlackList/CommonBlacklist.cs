using System;
using System.Collections.Generic;

namespace WordCloudImageGenerator.Parsing.BlackList
{
    public class CommonBlacklist : IBlackList
    {
        private readonly HashSet<string> excludedWordsHashSet;

        protected CommonBlacklist(IEnumerable<string> excludedWords)
            : this(excludedWords, StringComparer.InvariantCultureIgnoreCase)
        {
        }

        private CommonBlacklist(IEnumerable<string> excludedWords, StringComparer comparer)
        {
            excludedWordsHashSet = new HashSet<string>(excludedWords, comparer);
        }

        public bool Countains(string word)
        {
            return excludedWordsHashSet.Contains(word);
        }

        public int Count => excludedWordsHashSet.Count;
    }
}