using System.Collections.Generic;
using System.Drawing;

namespace TagCloud.CloudLayouter
{
    public interface ICloudLayouter
    {
        ImageSettings LayouterSize { get; }
        HashSet<Rectangle> Rectangles { get; set; }
        void RefreshLayouter();
        Rectangle PutNextRectangle(Size rectangleSize);
    }
}