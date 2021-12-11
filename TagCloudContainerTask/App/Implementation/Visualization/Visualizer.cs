using System.Collections.Generic;
using System.Drawing;
using App.Implementation.Words.Tags;
using App.Infrastructure.Visualization;

namespace App.Implementation.Visualization
{
    public class Visualizer : IVisualizer
    {
        private readonly IDrawer drawer;

        public Visualizer(IDrawer drawer)
        {
            this.drawer = drawer;
        }

        public Bitmap VisualizeCloud(Bitmap image, Point cloudCenter, IEnumerable<Tag> tags)
        {
            drawer.DrawTags(Graphics.FromImage(image), tags);
            return image;
        }

        public void VisualizeDebuggingMarkupOnImage(Image image, Point cloudCenter, int cloudCircleRadius)
        {
            var graphics = Graphics.FromImage(image);
            var imageSize = image.Size;

            drawer.DrawCanvasBoundary(graphics, imageSize);
            drawer.DrawAxis(graphics, imageSize, cloudCenter);
            drawer.DrawCloudBoundary(graphics, imageSize, cloudCenter, cloudCircleRadius);
        }
    }
}