using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagsCloudContainer.WordPreprocessors;

namespace TagsCloudContainer.WordCounters
{
    class SimpleWordCounter : IWordCounter
    {
        private IWordPreprocessor wordPreprocessor;

        public SimpleWordCounter(IWordPreprocessor wordPreprocessor)
        {
            this.wordPreprocessor = wordPreprocessor;
        }

        public List<WordToken> CountWords(string[] words)
        {
            var processedWords = wordPreprocessor.WordPreprocessing(words);

            var dict = new Dictionary<string, int>();
            foreach (var word in processedWords)
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
