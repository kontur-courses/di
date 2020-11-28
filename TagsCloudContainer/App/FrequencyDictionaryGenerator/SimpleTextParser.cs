using System.Collections.Generic;
using TagsCloudContainer.Infrastructure;

namespace TagsCloudContainer.App.FrequencyDictionaryGenerator
{
    internal class SimpleTextParser : ITextParser
    {
        public IEnumerable<string> GetWords(IEnumerable<string> lines)
        {
            return lines;
        }
    }
}