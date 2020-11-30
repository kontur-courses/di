using System.Collections.Generic;

namespace TagsCloudContainer.Infrastructure.DictionaryGenerator
{
    internal interface IWordFilter
    {
        public IEnumerable<string> FilterOutBoringWords(IEnumerable<string> words);
    }
}