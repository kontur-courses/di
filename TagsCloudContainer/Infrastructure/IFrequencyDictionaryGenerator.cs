using System.Collections.Generic;

namespace TagsCloudContainer.Infrastructure
{
    internal interface IFrequencyDictionaryGenerator
    {
        public Dictionary<string, double> GenerateFrequencyDictionary(IEnumerable<string> lines);
    }
}