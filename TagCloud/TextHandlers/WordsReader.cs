using System.Collections.Generic;
using TagCloud.TextHandlers.Converters;
using TagCloud.TextHandlers.Filters;
using TagCloud.TextHandlers.Parser;

namespace TagCloud.TextHandlers
{
    public class WordsReader : IReader
    {
        private readonly ITextParser parser;
        private readonly IConvertersPool converter;
        private readonly ITextFilter filter;

        public WordsReader(ITextParser parser, IConvertersPool converter, ITextFilter filter)
        {
            this.parser = parser;
            this.converter = converter;
            this.filter = filter;
        }

        public IEnumerable<string> Read(string filename)
        {
            var words = parser.GetWords(filename);
            var filtered = filter.Filter(words);
            var converted = converter.Convert(filtered);
            return converted;
        }
    }
}