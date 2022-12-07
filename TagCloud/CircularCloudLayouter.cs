using System;
using System.Drawing;
using System.Linq;
using TagCloud.Extensions;

namespace TagCloud
{
    public class CircularCloudLayouter
    {
        private readonly TagCloud tagCloud;

        private readonly SpiralPointGenerator spiralGenerator;


        public CircularCloudLayouter() : this(new Point())
        {
        }

        public CircularCloudLayouter(Point center)
        {
            tagCloud = new TagCloud(center);
            spiralGenerator = new SpiralPointGenerator(center);
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
            var nextPoint = spiralGenerator.GetNextPoint().ShiftTo(rectangleCenter);
            return nextPoint;
        }

        private Size GetCenterFor(Size rectangleSize) =>
            new Size(-rectangleSize.Width / 2, -rectangleSize.Height / 2);

        public TagCloud GetTagCloud() => tagCloud;
    }
}
