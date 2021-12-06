using System.Collections.Generic;
using System.Drawing;

namespace TagCloud.Visualization
{
    public interface IDrawer
    {
        void DrawCanvasBoundary(Graphics g, Size imgSize);

        void DrawAxis(Graphics g, Size imgSize, Point cloudCenter);

        void DrawCloudBoundary(Graphics g, Size imgSize, Point cloudCenter, int cloudCircleRadius);

        void DrawRectangles(Graphics g, List<Rectangle> rectangles);
    }
}