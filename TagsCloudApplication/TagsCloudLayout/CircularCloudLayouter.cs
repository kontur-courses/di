using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudLayout
{
    public class CircularCloudLayouter
    {
        private readonly Point center;
        private readonly ArchimedeanSpiral spiral;
        private readonly List<Rectangle> rectangles;

        public CircularCloudLayouter(Point center, 
            double stepLength=0.1, double angleShiftForEachPoint = 2 * Math.PI / 1000)
        {
            this.center = center;
            spiral = new ArchimedeanSpiral(center, stepLength, angleShiftForEachPoint);
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
                resultRectangle = new Rectangle(spiral.CalculateNextPoint(), rectangleSize)
                    .OffsetByMassCenter();

            return resultRectangle;
        }

        private bool IsIntersectingWithLayout(Rectangle rectangle)
        {
            return rectangles.Any(rect => rect.IntersectsWith(rectangle));
        }
    }
}
