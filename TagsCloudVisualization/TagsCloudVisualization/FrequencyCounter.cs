using System.Collections.Generic;
using System.Linq;
using TagsCloudVisualization.Interfaces;

namespace TagsCloudVisualization
{
    internal class FrequencyCounter<T> : IFrequencyCounter<T>
    {
        public FrequencyCounter(IFrequencyObjectSelector<T> selector)
        {
            Selector = selector;
        }

        public IFrequencyObjectSelector<T> Selector { set; private get; }

        public Dictionary<T, int> GetFrequencyDictionary(IEnumerable<T> objectSource)
        {
            return objectSource.Where(obj => Selector.Select(obj))
                .GroupBy(obj => obj)
                .ToDictionary(group => group.Key, group => group.Count());
        }
    }
}