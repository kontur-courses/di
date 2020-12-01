using System.Collections.Generic;

namespace TagsCloudContainer.Infrastructure.TextParserToFrequencyDictionary
{
    internal interface ITextParser
    {
        public IEnumerable<string> GetWords(IEnumerable<string> lines);
    }
}