using System.Collections.Generic;
using TagCloud.TextHandlers.Filters;
using TagsCloudVisualization;

namespace TagCloud.TextHandlers
{
    public class TextReaderFacade : IReader
    {
        private readonly ITextParser parser;
        private readonly IWordConverter converter;
        private readonly ITextFilter filter;

        public TextReaderFacade(ITextParser parser, IWordConverter converter, ITextFilter filter)
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