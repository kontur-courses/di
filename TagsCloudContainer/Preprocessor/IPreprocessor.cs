using System.Collections.Generic;
using TagsCloudContainer.Converter;
using TagsCloudContainer.Filter;

namespace TagsCloudContainer.Preprocessor
{
    public interface IPreprocessor
    {
        IEnumerable<string> PrepareWords(IEnumerable<string> words,
            IEnumerable<IWordsConverter> converters, IEnumerable<IFilter> filters);
    }
}