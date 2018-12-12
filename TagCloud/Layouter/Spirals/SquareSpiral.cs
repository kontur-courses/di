using System;
using TagCloud.Interfaces;

namespace TagCloud.Layouter.Spirals
{
    public class SquareSpiral : ISpiral
    {
        private double x;
        private double y;

        public Point Put(Point origin, double angle, double turnsInterval)
        {
            var degrees = ToDegrees(angle);
            var normalizedAngle = ExcludeFullTurns(degrees);

            var isInRightArc = normalizedAngle >= 0 && normalizedAngle <= 45
                               || normalizedAngle > 315 && normalizedAngle <= 360;
            var isInLeftArc = normalizedAngle > 135 && normalizedAngle <= 225;
            var isInTopArc = normalizedAngle > 45 && normalizedAngle <= 135;
            var isInBottomArc = normalizedAngle > 225 && normalizedAngle <= 315;

            if (isInLeftArc || isInRightArc)
                y = origin.Y + turnsInterval * angle * Math.Sin(angle);
            else if (isInTopArc || isInBottomArc)
                x = origin.X + turnsInterval * angle * Math.Cos(angle);

            return new Point(x, y);
        }

        private double ToDegrees(double radians)
        {
            return radians * 180 / Math.PI;
        }

        private double ExcludeFullTurns(double degrees)
        {
            return degrees - (int) degrees / 360 * 360;
        }
    }
}