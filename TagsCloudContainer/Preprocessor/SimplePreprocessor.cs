using System.Collections.Generic;
using System.Linq;
using TagsCloudContainer.Converter;
using TagsCloudContainer.Filter;

namespace TagsCloudContainer.Preprocessor
{
    public class SimplePreprocessor : IPreprocessor
    {
        public IEnumerable<string> Process(IEnumerable<string> words,
            IEnumerable<IConverter> converters, IEnumerable<IFilter> filters)
        {
            words = converters.Aggregate(words, (current, converter) => converter.Convert(current));

            return filters.Aggregate(words, (current, filter) => filter.Filtrate(current));
        }
    }
}