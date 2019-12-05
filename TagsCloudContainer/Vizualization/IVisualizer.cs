using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer
{
    public interface IVisualizer
    {
        void Visualize(IEnumerable<Rectangle> rectangles);
    }
}