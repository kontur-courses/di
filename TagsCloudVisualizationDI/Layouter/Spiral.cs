using System;
using System.Drawing;

namespace TagsCloudVisualizationDI.Layouter
{
    public class Spiral
    {
        private double CurrentAngle { get; set; }
        private double AngleStep { get; }


        public Spiral(double angleStep = Math.PI / 360)
        {
            CurrentAngle = 0;
            AngleStep = angleStep;
        }

        public Point GetNextPoint(Point center)
        {
            var nextX = center.X + CurrentAngle * Math.Cos(CurrentAngle);
            var nextY = center.Y + CurrentAngle * Math.Sin(CurrentAngle);
            var roundNextX = Convert.ToInt32(Math.Round(nextX));
            var roundNextY = Convert.ToInt32(Math.Round(nextY));
            var nextPoint = new Point(roundNextX, roundNextY);
            CurrentAngle += AngleStep;
            return nextPoint;
        }
    }
}
