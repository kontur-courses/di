using System;
using System.Drawing;

namespace TagsCloudContainer.Layouter.PointsProviders
{
    public class SpiralPointsProvider : IPointsProvider
    {
        private const double AngleDelta = Math.PI / 360;

        private readonly Point center;

        private double currentAngle;
        private Point? lastPoint;

        public SpiralPointsProvider(ITagCloudSettings settings)
        {
            center = new Point(settings.ImageWidth / 2, settings.ImageHeight / 2);
        }

        public LayoutAlrogorithm AlghorithmName => LayoutAlrogorithm.Circular;

        public Point GetNextPoint()
        {
            Point currentPoint;
            do
            {
                var radiusVector = currentAngle;
                var newX = center.X + radiusVector * Math.Cos(currentAngle);
                var newY = center.Y + radiusVector * Math.Sin(currentAngle);
                var roundedX = (int) Math.Round(newX);
                var roundedY = (int) Math.Round(newY);
                currentPoint = new Point(roundedX, roundedY);
                currentAngle += AngleDelta;
            } while (currentPoint == lastPoint);

            lastPoint = currentPoint;
            return currentPoint;
        }
    }
}