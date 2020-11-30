using System.Collections.Generic;

namespace TagsCloudContainer.Infrastructure.DictionaryGenerator
{
    internal interface IFrequencyDictionaryGenerator
    {
        public Dictionary<string, double> GenerateFrequencyDictionary(IEnumerable<string> lines);
    }
}