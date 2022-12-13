using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagCloud.PointGenerator;

namespace TagCloud.CloudLayouter
{
    public class CircularCloudLayouter : ICloudLayouter
    {
        public Point CloudCenter { get; }

        private readonly IPointGenerator pointGenerator;

        private readonly List<Rectangle> rectangles = new List<Rectangle>();

        public CircularCloudLayouter(IPointGenerator pointGenerator)
        {          
            this.pointGenerator = pointGenerator;

            CloudCenter = pointGenerator.CentralPoint;
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            if (!IsValidRectangleSize(rectangleSize))
                throw new ArgumentException("width and height of rectangle must be more than zero");

            Rectangle rectangle;

            do
            {
                var pointToPutRectangle = pointGenerator.GetNextPoint();

                rectangle = new Rectangle(pointToPutRectangle, rectangleSize);

            } while (IsIntersectWithAnyExistingRectangle(rectangle));

            var shiftedRectangle = ShiftRectangleToCenterPoint(rectangle);

            rectangles.Add(shiftedRectangle);

            return shiftedRectangle;
        }

        private static bool IsValidRectangleSize(Size rectangleSize)
        {
            return rectangleSize.Width > 0 && rectangleSize.Height > 0;
        }

        private bool IsIntersectWithAnyExistingRectangle(Rectangle rectangle)
        {
            return rectangles.Any(r => r.IntersectsWith(rectangle));
        }

        private Rectangle ShiftRectangleToCenterPoint(Rectangle rectangle)
        {
            var directionsToShift = GetDirectionsToShift(rectangle);

            var shiftedRectangleAlongX = ShiftRectangleAlongDirection(rectangle, directionsToShift.axisX);

            var shiftedRectangleAlongXAndY = ShiftRectangleAlongDirection(shiftedRectangleAlongX, directionsToShift.axisY);

            return shiftedRectangleAlongXAndY;
        }

        private (Vector axisX, Vector axisY) GetDirectionsToShift(Rectangle rectangle)
        {
            var deltaX = CloudCenter.X - rectangle.GetCenter().X > 0 ? 1 : -1;

            var deltaY = CloudCenter.Y - rectangle.GetCenter().Y > 0 ? 1 : -1;

            return (new Vector(deltaX, 0), new Vector(0, deltaY));
        }

        private Rectangle ShiftRectangleAlongDirection(Rectangle rectangle, Vector direction)
        {
            while (TryShiftRectangleAlongDirection(rectangle, direction, out var shiftedRectangle))
            {
                rectangle = shiftedRectangle;
            }

            return rectangle;
        }

        private bool TryShiftRectangleAlongDirection(Rectangle rectangle, Vector direction, out Rectangle shiftedRectangle)
        {
            if (IsRectangleAlignedAlongDirection(rectangle, direction))
            {
                shiftedRectangle = rectangle;

                return false;
            }

            shiftedRectangle = rectangle.MoveOn(direction.X, direction.Y);

            return !IsIntersectWithAnyExistingRectangle(shiftedRectangle);
        }

        private bool IsRectangleAlignedAlongDirection(Rectangle rectangle, Vector direction)
        {
            var vectorBetweenCenters = new Vector(CloudCenter, rectangle.GetCenter());

            return direction.IsPerpendicularTo(vectorBetweenCenters);
        }
    }
}
