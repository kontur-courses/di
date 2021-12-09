using System.Collections.Generic;
using System.Drawing;
using TagCloud.Words.Tags;

namespace TagCloud.Visualization
{
    public class Visualizer : IVisualizer
    {
        private readonly IDrawer drawer;

        public Visualizer(IDrawer drawer)
        {
            this.drawer = drawer;
        }

        public void VisualizeCloud(Graphics graphics, Point cloudCenter, IEnumerable<Tag> tags)
        {
            drawer.DrawTags(graphics, tags);
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