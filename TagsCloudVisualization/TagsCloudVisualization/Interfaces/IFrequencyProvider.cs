using System.Collections.Generic;

namespace TagsCloudVisualization.Interfaces
{
    internal interface IFrequencyProvider
    {
        Dictionary<string, int> GetFrequencyDictionary(IEnumerable<string> wordSource);
    }
}