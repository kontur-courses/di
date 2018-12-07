using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer
{
    public class CircularCloudLayouter : ICloudLayouter
    {
        private PointF Center { get; set; }
        private readonly List<RectangleF> placedRectangles = new List<RectangleF>();
        private const double ShiftOnSpiral = 0.01;
        private int rotationAngle;
        private readonly int rotationAngleStep;

        public CircularCloudLayouter(IConfiguration configuration)
        {
            rotationAngleStep = configuration.RotationAngle < 1 ? 1 : configuration.RotationAngle;
            Center = GetCenter(configuration);
        }

        private PointF GetCenter(IConfiguration configuration)
        {
            var imageCenterByAbscissa = configuration.ImageWidth / 2;
            var imageCenterByOrdinate = configuration.ImageHeight / 2;

            var userCenterByAbscissa = configuration.CenterX;
            var userCenterByOrdinate = configuration.CenterY;

            return new PointF(imageCenterByAbscissa + userCenterByAbscissa,
                imageCenterByOrdinate + userCenterByOrdinate);
        }

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