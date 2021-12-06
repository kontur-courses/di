using System;
using System.Collections.Generic;
using System.Linq;

namespace TagCloud.TextHandlers.Converters
{
    public class WordConverter : IWordConverter
    {
        private readonly List<Func<string, string>> converters;

        public WordConverter()
        {
            converters = new List<Func<string, string>>();
        }

        public WordConverter Using(Func<string, string> converter)
        {
            converters.Add(converter);
            return this;
        }

        public IEnumerable<string> Convert(IEnumerable<string> words)
        {
            return words.Select(Convert);
        }

        public string Convert(string word)
        {
            return converters.Aggregate(word, (current, converter) => converter(current));
        }
    }
}