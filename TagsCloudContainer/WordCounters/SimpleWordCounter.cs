using System.Collections.Generic;
using System.Linq;
using TagsCloudContainer.WordPreprocessors;
using TagsCloudContainer.WordFilters;
using TagsCloudContainer.TokensAndSettings;

namespace TagsCloudContainer.WordCounters
{
    public class SimpleWordCounter : IWordCounter
    {
        private IWordFilter wordFilter;

        public SimpleWordCounter(IWordFilter wordFilter)
        {
            this.wordFilter = wordFilter;
        }

        public List<WordToken> CountWords(ProcessedWord[] words)
        {
            var filteredWords = words
                .Where(word => wordFilter.IsCorrect(word))
                .Select(word => word.Word);

            var dict = new Dictionary<string, int>();
            foreach (var word in filteredWords)
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
