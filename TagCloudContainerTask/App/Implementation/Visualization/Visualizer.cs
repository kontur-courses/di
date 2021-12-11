using System.Collections.Generic;
using System.Drawing;
using App.Implementation.GeometryUtils;
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
            var relocatedRectangles = RelocateRectanglesToImageCenter(image.Size, tags);
            drawer.DrawTags(Graphics.FromImage(image), relocatedRectangles);
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

        private IEnumerable<Tag> RelocateRectanglesToImageCenter(Size size, IEnumerable<Tag> tags)
        {
            foreach (var tag in tags)
            {
                var wordOuterRectangle = tag.WordOuterRectangle;
                wordOuterRectangle.Location = tag.WordOuterRectangle.Location.MovePointToSizeCenter(size, true);
                tag.WordOuterRectangle = wordOuterRectangle;
            }

            return tags;
        }
    }
}