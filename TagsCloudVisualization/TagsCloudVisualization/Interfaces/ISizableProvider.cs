using System.Collections.Generic;

namespace TagsCloudVisualization.Interfaces
{
    public interface ISizableProvider<T>
    {
        ISizableSelector<T, int> Selector { set; }
        IEnumerable<Sizable<T>> GetSizableObjects(Dictionary<T, int> objects, int maxCountWords);
    }
}