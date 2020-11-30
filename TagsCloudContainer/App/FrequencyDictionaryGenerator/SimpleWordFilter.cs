using System.Collections.Generic;
using System.Linq;
using TagsCloudContainer.Infrastructure.DictionaryGenerator;

namespace TagsCloudContainer.App.FrequencyDictionaryGenerator
{
    internal class SimpleWordFilter : IWordFilter
    {
        public IEnumerable<string> FilterOutBoringWords(IEnumerable<string> words)
        {
            return words.Where(word => word.Length >= 4);
        }
    }
}