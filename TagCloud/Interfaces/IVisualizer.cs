using System.Collections.Generic;
using System.Drawing;
using TagCloud.IntermediateClasses;
using TagCloud.Visualizer;

namespace TagCloud.Interfaces
{
    public interface IVisualizer
    {
        Image Visualize(IEnumerable<PositionedElement> objects);
    }
}