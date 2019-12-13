using System.Collections.Generic;

namespace TagsCloudVisualization.Interfaces
{
    internal interface IFrequencyProvider<T>
    {
        Dictionary<T, int> GetFrequencyDictionary(IEnumerable<T> objects);
    }
}