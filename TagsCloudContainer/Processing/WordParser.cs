using System;
using System.Collections.Generic;
using System.Linq;
using TagsCloudContainer.Processing.Converting;
using TagsCloudContainer.Processing.Filtering;

namespace TagsCloudContainer.Input
{
    public class WordParser
    {
        private static readonly char[] Whitespaces = {' ', '\r', '\n', '\t'};

        private readonly IWordFilter[] filters;
        private readonly IWordConverter[] converters;

        public WordParser(IWordFilter[] filters, IWordConverter[] converters)
        {
            this.filters = filters;
            this.converters = converters;
        }

        public Dictionary<string, int> ParseWords(string input)
        {
            IEnumerable<string> words = string.Concat(input.Where(c => !char.IsPunctuation(c)))
                .Split(Whitespaces, StringSplitOptions.RemoveEmptyEntries)
                .Select(w => w.ToLower());

            words = FilterWords(ConvertWords(words));

            var wordCount = new Dictionary<string, int>();
            foreach (var word in words)
            {
                if (!wordCount.ContainsKey(word))
                    wordCount[word] = 0;
                wordCount[word]++;
            }

            return wordCount;
        }

        private IEnumerable<string> ConvertWords(IEnumerable<string> words)
        {
            return converters.Aggregate(words, (current, converter) => converter.Convert(current));
        }

        private IEnumerable<string> FilterWords(IEnumerable<string> words)
        {
            return filters.Aggregate(words, (current, filter) => filter.Filter(current));
        }
    }
}