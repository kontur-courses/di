using System.Collections.Generic;
using System.Drawing;

namespace WordCloudImageGenerator.Layouting.Cloud
{
    public interface IRectangleCloud
    {
        List<Rectangle> Rectangles { get; }
    }
}