using System;
using System.Drawing;
using System.Linq;
using TagCloud.Extensions;
using TagCloud.PointGenerators;

namespace TagCloud
{
    public class CircularCloudLayouter
    {
        private readonly TagCloud tagCloud;

        private readonly IPointGenerator pointGenerator;

        public CircularCloudLayouter(IPointGenerator pointGenerator)
        {
            tagCloud = new TagCloud(pointGenerator.GetCenterPoint());
            this.pointGenerator = pointGenerator;
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
            while (tagCloud.Rectangles.Any(r => r.IntersectsWith(rectangle)));
            tagCloud.Rectangles.Add(rectangle);

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

        public TagCloud GetTagCloud() => tagCloud;
    }
}
