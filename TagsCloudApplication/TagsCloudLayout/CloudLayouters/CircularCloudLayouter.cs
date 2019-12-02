using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudLayout.PointLayouters;

namespace TagsCloudLayout.CloudLayouters
{
    public class CircularCloudLayouter: ICloudLayouter
    {
        private readonly Point center;
        private readonly ICircularPointLayouter pointLayouter;
        private readonly List<Rectangle> rectangles;

        public CircularCloudLayouter(ICircularPointLayouter pointLayouter)
        {
            center = pointLayouter.Center;
            this.pointLayouter = pointLayouter;
            rectangles = new List<Rectangle>();
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            if (rectangleSize.Height <= 0 || rectangleSize.Width <= 0)
                throw new ArgumentException(
                    "Height and width of rectangle size must be greater than zero!");
            var resultRectangle = FindRectanglePosition(rectangleSize);
            rectangles.Add(resultRectangle);

            return resultRectangle;
        }

        private Rectangle FindRectanglePosition(Size rectangleSize)
        {
            var resultRectangle = new Rectangle(center, rectangleSize)
                .OffsetByMassCenter();
            while (IsIntersectingWithLayout(resultRectangle))
                resultRectangle = new Rectangle(pointLayouter.CalculateNextPoint(), rectangleSize)
                    .OffsetByMassCenter();

            return resultRectangle;
        }

        private bool IsIntersectingWithLayout(Rectangle rectangle)
        {
            return rectangles.Any(rect => rect.IntersectsWith(rectangle));
        }
    }
}
