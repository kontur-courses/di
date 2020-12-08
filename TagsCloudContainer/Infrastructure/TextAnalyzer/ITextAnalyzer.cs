using System.Collections.Generic;

namespace TagsCloudContainer.Infrastructure.TextAnalyzer
{
    internal interface ITextAnalyzer
    {
        public Dictionary<string, double> GenerateFrequencyDictionary(IEnumerable<string> lines);
    }
}