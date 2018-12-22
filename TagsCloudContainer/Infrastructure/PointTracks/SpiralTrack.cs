using System;
using System.Drawing;

namespace TagsCloudContainer.Infrastructure.PointTracks
{
    public class SpiralTrack : IPointsTrack
    {
        private readonly Point center;
        private int angle;
        private readonly double step;
        private Point? lastPoint;

        public SpiralTrack(Point center, double step)
        {
            this.center = center;
            this.step = step;
        }

        public Point GetNextPoint()
        {
            var nextPoint = lastPoint;

            if (lastPoint == null)
            {
                lastPoint = center;
                return center;
            }

            while (nextPoint == lastPoint)
            {
                var vectorLength = angle * (step / (2 * Math.PI));
                var radiusVector = (
                    X: (int)Math.Ceiling(vectorLength * Math.Cos(angle)),
                    Y: (int)Math.Ceiling(vectorLength * Math.Sin(angle))
                );

                lastPoint = nextPoint;
                nextPoint = new Point(center.X + radiusVector.X, center.Y + radiusVector.Y);
                angle += 1;
            }

            if (nextPoint.HasValue)
                return nextPoint.Value;

            throw new NotImplementedException();
        }
    }
}
