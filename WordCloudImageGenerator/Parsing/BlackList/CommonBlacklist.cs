using System;
using System.Collections.Generic;

namespace WordCloudImageGenerator.Parsing.BlackList
{
    public class CommonBlacklist : IBlackList
    {
        private readonly HashSet<string> excludedWordsHashSet;

        public CommonBlacklist(IEnumerable<string> excludedWords)
            : this(excludedWords, StringComparer.InvariantCultureIgnoreCase)
        {
            
        }
        
        public CommonBlacklist(IEnumerable<string> excludedWords, StringComparer comparer)
        {
            excludedWordsHashSet = new HashSet<string>(excludedWords, comparer);
        }

        public bool Countains(string word)
        {
            return excludedWordsHashSet.Contains(word);
        }

        public int Count
        {
            get { return excludedWordsHashSet.Count; }
        }
    }
}