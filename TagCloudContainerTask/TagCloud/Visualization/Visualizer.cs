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

        public void VisualizeCloud(Graphics g, Point cloudCenter, List<Rectangle> rectangles)
        {
            drawer.DrawRectangles(g, rectangles);
        }

        public void VisualizeDebuggingMarkup(Graphics g, Size imgSize,
            Point cloudCenter, int cloudCircleRadius)
        {
            drawer.DrawCanvasBoundary(g, imgSize);
            drawer.DrawAxis(g, imgSize, cloudCenter);
            drawer.DrawCloudBoundary(g, imgSize, cloudCenter, cloudCircleRadius);
        }
    }
}