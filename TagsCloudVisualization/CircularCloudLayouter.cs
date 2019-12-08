using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization
{
    public class CircularCloudLayouter
    {
        public Point Center { get; }
        private readonly Spiral spiral;
        private readonly List<Rectangle> rectangles;

        public CircularCloudLayouter(Point center)
        {
            Center = center;
            rectangles = new List<Rectangle>();
            spiral = new Spiral(0.25, 1);
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
