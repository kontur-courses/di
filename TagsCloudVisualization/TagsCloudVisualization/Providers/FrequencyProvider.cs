using System.Collections.Generic;
using System.Linq;
using TagsCloudVisualization.Interfaces;

namespace TagsCloudVisualization.Providers
{
    internal class FrequencyProvider<T> : IFrequencyProvider<T>
    {
        public Dictionary<T, int> GetFrequencyDictionary(IEnumerable<T> objectSource)
        {
            return objectSource
                .GroupBy(obj => obj)
                .ToDictionary(group => group.Key, group => group.Count());
        }
    }
}