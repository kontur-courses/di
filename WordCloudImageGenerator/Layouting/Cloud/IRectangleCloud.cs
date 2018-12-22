using System.Collections.Generic;
using System.Drawing;

namespace WordCloudImageGenerator.LayoutCraetion.Cloud
{
    public interface IRectangleCloud
    {
        List<Rectangle> Rectangles { get; }
    }
}