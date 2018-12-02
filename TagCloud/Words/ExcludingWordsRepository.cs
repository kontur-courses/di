using System;
using System.Collections.Generic;

namespace TagCloud.Words
{
    public class ExcludingWordsRepository
    {
        private readonly HashSet<string> excludingWords = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        
        public void Load(IEnumerable<string> words)
        {
            excludingWords.Clear();
            excludingWords.UnionWith(words);
        }
        
        public bool Contains(string word)
        {
            return excludingWords.Contains(word);
        }
    }
}