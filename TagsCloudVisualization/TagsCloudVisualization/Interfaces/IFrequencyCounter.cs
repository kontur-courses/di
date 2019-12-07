using System.Collections.Generic;

namespace TagsCloudVisualization.Interfaces
{
    internal interface IFrequencyCounter<T>
    {
        IFrequencyObjectSelector<T> Selector { set; }
        Dictionary<T, int> GetFrequencyDictionary(IEnumerable<T> objects);
    }
}