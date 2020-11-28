using System.Collections.Generic;

namespace TagsCloudContainer.Infrastructure
{
    internal interface IWordFilter
    {
        public IEnumerable<string> FilterOutBoringWords(IEnumerable<string> words);
    }
}