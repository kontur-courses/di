using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer
{
    public interface ICloudLayouter
    {
        List<Rectangle> GenerateCloud(Point center, List<Size> rectangleSizes);
        Rectangle GetNextRectangle(Point center, List<Rectangle> rectangles, Size rectangleSize);
    }
}