using System.Collections.Generic;

namespace TagsCloudContainer.Word_Counting
{
    public class WordCounter : IWordCounter
    {
        private readonly IWordFilter filter;
        private readonly IWordNormalizer normalizer;

        public WordCounter(IWordFilter filter, IWordNormalizer normalizer)
        {
            this.filter = filter;
            this.normalizer = normalizer;
        }

        public Dictionary<string, int> CountWords(IEnumerable<string> words)
        {
            var resultDictionary = new Dictionary<string, int>();
            foreach (var word in words)
            {
                var normalizedWord = normalizer.Normalize(word);
                if (filter.IsExcluded(normalizedWord))
                    continue;
                if (resultDictionary.ContainsKey(normalizedWord))
                    resultDictionary[normalizedWord]++;
                else
                    resultDictionary[normalizedWord] = 1;
            }

            return resultDictionary;
        }
    }
}