using System.Collections.Generic;

namespace TagsCloudContainer.Infrastructure.DictionaryGenerator
{
    internal interface ITextParser
    {
        public IEnumerable<string> GetWords(IEnumerable<string> lines);
    }
}