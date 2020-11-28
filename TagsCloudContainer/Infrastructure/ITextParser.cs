using System.Collections.Generic;

namespace TagsCloudContainer.Infrastructure
{
    internal interface ITextParser
    {
        public IEnumerable<string> GetWords(IEnumerable<string> lines);
    }
}