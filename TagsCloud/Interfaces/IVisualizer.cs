using System.Collections.Generic;
using System.Drawing;
using TagsCloudVisualization.Visualizator;
using TagsCloudVisualization.Visualizer;

namespace TagsCloudVisualization.Interfaces
{
    public interface IVisualizer
    {
        void Visualize(IEnumerable<VisualElement> objects, Size size);
    }
}