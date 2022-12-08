using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagCloud.Extensions;
using TagCloud.PointGenerators;
using TagCloud.Tags;
using TagCloud.WordPreprocessors;

namespace TagCloud.CloudLayouters
{
    public class CircularCloudLayouter : ICloudLayouter
    {
        public Point Center => pointGenerator.GetCenterPoint();

        private readonly IPointGenerator pointGenerator;
        private readonly List<Rectangle> rectangles;

        public CircularCloudLayouter(IPointGenerator pointGenerator)
        {
            this.pointGenerator = pointGenerator;
            this.rectangles = new List<Rectangle>();
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

        public TagCloud GetTagCloudOfLayout()
        {
            var tagCloud = new TagCloud(pointGenerator.GetCenterPoint());
            tagCloud.Rectangles.AddRange(rectangles.Select(rectangle => new Layout(rectangle)));
            return tagCloud;
        }
    }
}
