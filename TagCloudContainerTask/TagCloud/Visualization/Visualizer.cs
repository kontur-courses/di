using System.Collections.Generic;
using System.Drawing;

namespace TagCloud.Visualization
{
    public class Visualizer : IVisualizer
    {
        private readonly IDrawer drawer;

        public Visualizer(IDrawer drawer)
        {
            this.drawer = drawer;
        }

        public void VisualizeCloud(Graphics graphics, Point cloudCenter, List<Rectangle> rectangles)
        {
            drawer.DrawRectangles(graphics, rectangles);
        }

        public void VisualizeDebuggingMarkup(Graphics graphics, Size imgSize,
            Point cloudCenter, int cloudCircleRadius)
        {
            drawer.DrawCanvasBoundary(graphics, imgSize);
            drawer.DrawAxis(graphics, imgSize, cloudCenter);
            drawer.DrawCloudBoundary(graphics, imgSize, cloudCenter, cloudCircleRadius);
        }
    }
}