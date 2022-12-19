using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagCloud.Extensions;
using TagCloud.PointGenerators;

namespace TagCloud.CloudLayouters
{
    public class CircularCloudLayouter : ICloudLayouter
    {
        public Point Center { get; }

        private readonly IPointGenerator pointGenerator;
        private readonly List<Rectangle> rectangles;

        public delegate ICloudLayouter Factory(IPointGenerator.Factory pointGeneratorFactory);

        public CircularCloudLayouter(IPointGenerator.Factory pointGeneratorFactory)
        {
            rectangles = new List<Rectangle>();
            pointGenerator = pointGeneratorFactory.Invoke();
            Center = pointGenerator.GetCenterPoint();
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            if (rectangleSize.Height < 1 || rectangleSize.Width < 1)
                throw new ArgumentException("width and height of the rectangle must be greater than zero");

            Rectangle rectangle;
            do
            {
                rectangle = GetNextRectangle(rectangleSize);
            }
            while (rectangles.Any(r => r.IntersectsWith(rectangle)));
            rectangles.Add(rectangle);
            return rectangle;
        }

        private Rectangle GetNextRectangle(Size rectangleSize) =>
            new Rectangle(GetNextRectanglePoint(rectangleSize), rectangleSize);

        private Point GetNextRectanglePoint(Size rectangleSize)
        {
            var rectangleCenter = GetCenterFor(rectangleSize);
            var nextPoint = pointGenerator.GetNextPoint().ShiftTo(rectangleCenter);
            return nextPoint;
        }

        private Size GetCenterFor(Size rectangleSize) =>
            new Size(-rectangleSize.Width / 2, -rectangleSize.Height / 2);
    }
}
