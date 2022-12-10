using System.Collections.Generic;
using TagsCloudVisualization.Infrastructure.Parsers;

namespace TagsCloudVisualization.Infrastructure
{
    public interface IAnalyzer
    {
        public Dictionary<string, int> CreateFrequencyDictionary();

        void SetParser(IParser parser, string filePath);
    }
}