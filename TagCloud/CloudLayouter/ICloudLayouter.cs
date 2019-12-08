using System.Collections.Generic;
using System.Drawing;
using TagCloud.Visualization;

namespace TagCloud.CloudLayouter
{
    public interface ICloudLayouter
    {
        ImageSettings LayouterSettings { get; }
        HashSet<Rectangle> Rectangles { get; set; }
        void ResetLayouter();
        bool TryPutNextRectangle(Size rectangleSize, out Rectangle outRectangle);
    }
}