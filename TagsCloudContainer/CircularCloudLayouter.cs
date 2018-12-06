using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer
{
    public class CircularCloudLayouter : ICloudLayouter
    {
        public Point Center { get; private set; }
        private readonly List<RectangleF> placedRectangles = new List<RectangleF>();
        private const double ShiftOnSpiral = 0.01;
        private int rotationAngle;
        private readonly int rotationAngleStep;

        public CircularCloudLayouter(IConfiguration configuration)
        {
            rotationAngleStep = configuration.RotationAngle < 1 ? 1 : configuration.RotationAngle;
        }

        public void SetCenter(Point center) => Center = center;

        public RectangleF PutNextRectangleF(SizeF rectangleSize)
        {
            CheckRectangleSizeCorrectness(rectangleSize);

            while (true)
            {
                var possibleNextRectangle = GetNextPossibleRectangle(rectangleSize);

                if (placedRectangles.Any(r => r.IntersectsWith(possibleNextRectangle)))
                    continue;

                placedRectangles.Add(possibleNextRectangle);

                return possibleNextRectangle;
            }
        }

        private RectangleF GetNextPossibleRectangle(SizeF rectangleSize) => new RectangleF(GetNextPointOnSpiral(), rectangleSize);

        private PointF GetNextPointOnSpiral()
        {
            var dx = Math.Cos(rotationAngle) * rotationAngle * ShiftOnSpiral;
            var dy = Math.Sin(rotationAngle) * rotationAngle * ShiftOnSpiral;

            rotationAngle += rotationAngleStep;

            var nextX = Center.X + (float) dx;
            var nextY = Center.Y + (float) dy;

            return new PointF(nextX, nextY);
        }

        private static void CheckRectangleSizeCorrectness(SizeF rectangleSize)
        {
            if (rectangleSize.Width <= 0 || rectangleSize.Height <= 0)
                throw new ArgumentException();
        }
    }
}