using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization
{
    public interface ICloudLayouter
    {
        Rectangle GetNextRectangle(List<Rectangle> rectangles, Size nextRectangleSize);
        List<Rectangle> GenerateCloud(List<Size> rectangleSizes);
    }
}