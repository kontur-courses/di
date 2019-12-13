using System;
using System.Drawing;
using TagsCloud.Interfaces;

namespace TagsCloud.CloudLayouters
{
    public class RoundSpiralPositionGenerator : IPositionGenerator
    {
        private Point center = new Point(0, 0);
        private double angle = 0;
        public readonly double DeltaRadiusBetweenTurns;
        public readonly double DeltaAngle = 0.5;

        public RoundSpiralPositionGenerator(double deltaRadiusBetweenTurns = Math.PI)
        {
            this.DeltaRadiusBetweenTurns = deltaRadiusBetweenTurns;
        }

        public Point GetNextPosition()
        {
            angle += DeltaAngle;
            var dist = DeltaRadiusBetweenTurns * angle / 2 / Math.PI;
            var X = (int)(center.X + (dist * Math.Cos(angle)));
            var Y = (int)(center.Y + (dist * Math.Sin(angle)));
            return new Point(X, Y);
        }
    }
}
