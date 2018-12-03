using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagCloud.Interfaces;
using TagCloud.TagCloudVisualization.Extensions;

namespace TagCloud.TagCloudVisualization.Layouter
{
    public class CircularCloudLayouter : ICloudLayouter    
    {
        private List<Rectangle> Rectangles;
        private IEnumerable<Point> spiralPoints;
        private Point center = new Point(0, 0);

        public CircularCloudLayouter()
        {
            Rectangles = new List<Rectangle>();
            spiralPoints = new Spiral().GenerateRectangleLocation();
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            var rectangle = GenerateNewRectangle(rectangleSize);
            Rectangles.Add(rectangle);
            return rectangle;
        }

        private Rectangle GenerateNewRectangle(Size rectangleSize)
        {
            Rectangle rectangle = new Rectangle();
            foreach (var rectangleCenterPointLocation in spiralPoints)
            {
                var rectangleLocation = rectangleCenterPointLocation.ShiftToLeftRectangleCorner(rectangleSize);
                rectangle = new Rectangle(rectangleLocation, rectangleSize);
                if (RectanglesDoNotIntersect(rectangle))
                    break;
            }
            return rectangle;
        }

        private bool RectanglesDoNotIntersect(Rectangle newRectangle)
        {
            return !(Rectangles.Any(newRectangle.IntersectsWith));
        }

        public void Clear()
        {
            Rectangles.Clear();
            spiralPoints = new Spiral().GenerateRectangleLocation();
        }
    }
}
