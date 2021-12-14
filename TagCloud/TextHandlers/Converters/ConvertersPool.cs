using System;
using System.Collections.Generic;
using System.Linq;

namespace TagCloud.TextHandlers.Converters
{
    public class ConvertersPool : IConvertersPool
    {
        private readonly List<Func<string, string>> converters;

        public ConvertersPool(IConverter[] converters)
        {
            this.converters = new List<Func<string, string>>();
            foreach (var converter in converters)
            {
                this.converters.Add(converter.Convert);
            }
        }

        public ConvertersPool Using(Func<string, string> converter)
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