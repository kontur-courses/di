using System.Collections.Generic;
using System.Linq;
using TagsCloudContainer.Converter;
using TagsCloudContainer.Filter;

namespace TagsCloudContainer.Preprocessor
{
    public class SimplePreprocessor : IPreprocessor
    {
        private readonly IEnumerable<IWordsConverter> converters;
        private readonly IEnumerable<IFilter> filters;

        public SimplePreprocessor(IEnumerable<IWordsConverter> converters, IEnumerable<IFilter> filters)
        {
            this.converters = converters;
            this.filters = filters;
        }

        public IEnumerable<string> PrepareWords(IEnumerable<string> words)
        {
            words = converters.Aggregate(words, (current, converter) => converter.Convert(current));

            return filters.Aggregate(words, (current, filter) => filter.FilterOut(current));
        }
    }
}