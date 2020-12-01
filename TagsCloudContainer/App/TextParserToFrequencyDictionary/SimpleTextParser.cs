using System.Collections.Generic;
using TagsCloudContainer.Infrastructure.TextParserToFrequencyDictionary;

namespace TagsCloudContainer.App.TextParserToFrequencyDictionary
{
    internal class SimpleTextParser : ITextParser
    {
        public IEnumerable<string> GetWords(IEnumerable<string> lines)
        {
            return lines;
        }
    }
}