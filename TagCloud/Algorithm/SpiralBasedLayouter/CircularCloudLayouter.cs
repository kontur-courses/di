using System.Collections.Generic;
using System.Drawing;
using TagCloud.Infrastructure;

namespace TagCloud.Algorithm.SpiralBasedLayouter
{
    public class CircularCloudLayouter : ITagCloudLayouter
    {
        public Point Center { get; }
        private readonly Spiral spiral;
        private readonly List<Rectangle> rectangles;

        public CircularCloudLayouter(PictureConfig pictureConfig)
        {
            Center = pictureConfig.Center;
            rectangles = new List<Rectangle>();
            spiral = new Spiral(0.25, 1, Center);
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            while (true)
            {
                var currentPoint = spiral.GetNextPoint();
                var rectangle = RectangleUtils.GetClosestRectangleThatDoesNotIntersectWithOthers(
                    currentPoint, rectangleSize, Center, rectangles);
                if (rectangle == null)
                    continue;
                rectangles.Add(rectangle.Value);
                return rectangle.Value;
            }
        }
    }
}
