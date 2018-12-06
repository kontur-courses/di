using System.Collections.Generic;
using TagCloud.Visualizer;

namespace TagCloud.Interfaces
{
    public interface IVisualizer
    {
        void Visualize(IEnumerable<VisualElement> objects);
    }
}