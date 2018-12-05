using System.Collections.Generic;
using TagsCloudVisualization.Visualizer;

namespace TagsCloudVisualization.Interfaces
{
    public interface IVisualizer
    {
        void Visualize(IEnumerable<VisualElement> objects);
    }
}