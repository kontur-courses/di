using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagsCloudContainer.WordPreprocessors;
using TagsCloudContainer.WordFilters;

namespace TagsCloudContainer.WordCounters
{
    class SimpleWordCounter : IWordCounter
    {
        private IWordPreprocessor wordPreprocessor;
        private IWordFilter wordFilter;

        public SimpleWordCounter(IWordPreprocessor wordPreprocessor, IWordFilter wordFilter)
        {
            this.wordPreprocessor = wordPreprocessor;
            this.wordFilter = wordFilter;
        }

        public List<WordToken> CountWords(string[] words)
        {
            var processedWords = words
                .Where(word => wordFilter.IsCorrect(word))
                .Select(word => wordPreprocessor.WordPreprocessing(word));

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
