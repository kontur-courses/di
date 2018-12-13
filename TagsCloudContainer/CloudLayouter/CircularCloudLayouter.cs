using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudContainer.CloudLayouter
{
    public class CircularCloudLayouter : ICloudLayouter
    {
        private ICloudLayouterSettings Settings { get; }
        private PointF? center;
        private PointF Center => center ?? (PointF) (center = GetCenter(Settings));
        private readonly List<RectangleF> placedRectangles = new List<RectangleF>();
        private const double ShiftOnSpiral = 0.01;
        private int rotationAngle;
        private int? rotationAngleStep;

        private int RotationAngleStep =>
            rotationAngleStep ?? (int) (rotationAngleStep = GetRotationAngleStep(Settings));

        public CircularCloudLayouter(ICloudLayouterSettings settings)
        {
            Settings = settings;
        }

        private PointF GetCenter(ICloudLayouterSettings settings)
        {
            var imageCenterByAbscissa = settings.ImageWidth / 2;
            var imageCenterByOrdinate = settings.ImageHeight / 2;

            var userCenterByAbscissa = settings.CenterX;
            var userCenterByOrdinate = settings.CenterY;

            return new PointF(imageCenterByAbscissa + userCenterByAbscissa,
                imageCenterByOrdinate + userCenterByOrdinate);
        }

        private int GetRotationAngleStep(ICloudLayouterSettings settings)
        {
            return settings.RotationAngle < 1 ? 1 : settings.RotationAngle;
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

            rotationAngle += RotationAngleStep;

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