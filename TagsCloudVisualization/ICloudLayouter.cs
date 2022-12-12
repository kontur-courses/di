using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization
{
    public interface ICloudLayouter
    {
        Rectangle GetNextRectangle(Point center, List<Rectangle> rectangles, Size nextRectangleSize);
        List<Rectangle> GenerateCloud(Point center, List<Size> rectangleSizes);
    }
}