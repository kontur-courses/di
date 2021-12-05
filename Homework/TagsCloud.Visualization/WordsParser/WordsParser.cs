using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TagsCloud.Visualization.WordsFilter;

namespace TagsCloud.Visualization.WordsParser
{
    public class WordsParser : IWordsParser
    {
        private const string WordsPattern = @"\W+";
        private readonly IEnumerable<IWordsFilter> wordsFilters;

        public WordsParser(IEnumerable<IWordsFilter> wordsFilters) => this.wordsFilters = wordsFilters;

        public Dictionary<string, int> CountWordsFrequency(string text)
        {
            if (text == null)
                throw new ArgumentNullException(nameof(text));

            return Regex.Split(text.ToLower(), WordsPattern)
                .Where(w => w.Length > 1 && wordsFilters.All(x => x.IsWordValid(w)))
                .GroupBy(s => s)
                .ToDictionary(x => x.Key, x => x.Count());
        }
    }
}