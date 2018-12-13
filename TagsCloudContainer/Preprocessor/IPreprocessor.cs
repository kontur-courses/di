using System.Collections.Generic;
using TagsCloudContainer.Converter;
using TagsCloudContainer.Filter;

namespace TagsCloudContainer.Preprocessor
{
    public interface IPreprocessor
    {
        IEnumerable<string> Process(IEnumerable<string> words,
            IEnumerable<IConverter> converters, IEnumerable<IFilter> filters);
    }
}