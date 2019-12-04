using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization
{
    public class BitmapGenerator
    {
        public static Bitmap CreateBitmap(Point center, Size pictureSize, IEnumerable<Size> sizes)
        {
            var rectangles = new CircularCloudLayouter(center, pictureSize).PutMultipleRectangles(sizes);
            var visualizer = new Visualizer(pictureSize);
            return visualizer.GetRectangleLayoutBitmap(rectangles);
        }
    }
}