using System.Collections.Generic;

namespace TagsCloudContainer.Infrastructure.TextParserToFrequencyDictionary
{
    internal interface ITextParserToFrequencyDictionary
    {
        public Dictionary<string, double> GenerateFrequencyDictionary(IEnumerable<string> lines);
    }
}