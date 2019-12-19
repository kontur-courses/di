using System.Collections.Generic;
using System.Linq;
using TagsCloudVisualization.Interfaces;

namespace TagsCloudVisualization.Providers
{
    internal class WordFrequencyProvider : IFrequencyProvider
    {
        public Dictionary<string, int> GetFrequencyDictionary(IEnumerable<string> wordSource)
        {
            return wordSource
                .GroupBy(word => word)
                .ToDictionary(group => group.Key, group => group.Count());
        }
    }
}