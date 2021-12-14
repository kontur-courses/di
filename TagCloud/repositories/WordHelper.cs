using System.Collections.Generic;
using System.Linq;
using TagCloud.selectors;

namespace TagCloud.repositories
{
    public class WordHelper : IWordHelper
    {
        private readonly IFilter<string> filter;
        private readonly IConverter<IEnumerable<string>> converter;

        public WordHelper(IFilter<string> filter, IConverter<IEnumerable<string>> converter)
        {
            this.filter = filter;
            this.converter = converter;
        }

        public IEnumerable<WordStatistic> GetWordStatistics(IEnumerable<string> words)
        {
            var statistics = new Dictionary<string, int>();
            foreach (var word in words)
            {
                if (!statistics.ContainsKey(word))
                    statistics.Add(word, 0);
                statistics[word]++;
            }

            return statistics.Select(s => new WordStatistic(s.Key, s.Value))
                .OrderBy(s => s.Count);
        }

        public IEnumerable<string> FilterWords(IEnumerable<string> words) => filter.Filter(words);

        public IEnumerable<string> ConvertWords(IEnumerable<string> words) => converter.Convert(words);
    }
}