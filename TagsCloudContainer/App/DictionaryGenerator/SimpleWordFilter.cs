using System.Collections.Generic;
using System.Linq;
using TagsCloudContainer.Infrastructure;

namespace TagsCloudContainer.App.DictionaryGenerator
{
    internal class SimpleWordFilter : IWordFilter
    {
        public IEnumerable<string> FilterOutBoringWords(IEnumerable<string> words)
        {
            return words.Where(word => word.Length >= 4);
        }
    }
}