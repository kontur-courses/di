using System.Collections.Generic;
using System.Drawing;

namespace TagCloud.Visualization
{
    public interface IDrawer
    {
        void DrawCanvasBoundary(Graphics graphics, Size imgSize);

        void DrawAxis(Graphics graphics, Size imgSize, Point cloudCenter);

        void DrawCloudBoundary(Graphics graphics, Size imgSize, Point cloudCenter, int cloudCircleRadius);

        void DrawRectangles(Graphics graphics, List<Rectangle> rectangles);
    }
}