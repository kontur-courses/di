using System.Collections.Generic;
using System.Drawing;

namespace WordCloud.LayoutGeneration.Cloud
{
    public interface IRectangleCloud
    {
        List<Rectangle> Rectangles { get; set; }
    }
}