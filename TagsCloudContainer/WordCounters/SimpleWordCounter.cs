using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer.WordCounters
{
    class SimpleWordCounter : IWordCounter
    {
        public List<WordToken> CountWords(IEnumerable<string> words)
        {
            var dict = new Dictionary<string, int>();
            foreach (var word in words)
            {
                if (!dict.ContainsKey(word))
                    dict[word] = 0;
                dict[word]++;
            }

            return dict
                .ToList()
                .Select(keyValuePair => new WordToken(keyValuePair.Key, keyValuePair.Value))
                .OrderByDescending(tuple => tuple.Count)
                .ToList();
        }
    }
}
