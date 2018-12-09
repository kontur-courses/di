using System.Collections.Generic;
using System.Drawing;
using TagCloud.IntermediateClasses;

namespace TagCloud.Interfaces
{
    public interface IVisualizer
    {
        Image Visualize(IEnumerable<PositionedElement> objects);
    }
}