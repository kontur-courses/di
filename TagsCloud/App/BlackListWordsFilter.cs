using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TagsCloud.App
{
    public class BlackListWordsFilter : IWordsFilter
    {
        private readonly HashSet<string> blackList;

        public BlackListWordsFilter(HashSet<string> excludedWords)
        {
            blackList = excludedWords;
        }

        public IEnumerable<string> FilterWords(IEnumerable<string> words)
        {
            return words.Where(word => !blackList.Contains(word));
        }
    }
}
