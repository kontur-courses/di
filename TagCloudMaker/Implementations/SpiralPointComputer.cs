using System;
using System.Drawing;
using TagCloud.Interfaces;

namespace TagCloud.Implementations
{
    public class SpiralPointComputer: IPointComputer
    {
        private readonly Point center;
        private double currentRadius;
        private double currentAngle;

        public SpiralPointComputer(Point center)
        {
            this.center = center;
        }
        
        public Point GetNextPoint(double radiusStep, double angleStep)
        {
            RadiusCheck(radiusStep);

            UpdateAngleAndRadius(radiusStep, angleStep);

            var x = center.X + (int)Math.Round(currentRadius * Math.Cos(currentAngle));
            var y = center.Y + (int)Math.Round(currentRadius * Math.Sin(currentAngle));
            return new Point(x, y);
        }

        private void UpdateAngleAndRadius(double radiusStep, double angleStep)
        {
            var angleStepInRadians = angleStep * Math.PI / 360;
            currentAngle = (currentAngle + angleStepInRadians) % (Math.PI * 2);
            currentRadius += radiusStep;
        }

        private void RadiusCheck(double radiusStep)
        {
            if (currentRadius + radiusStep < 0)
                throw new ArgumentException($"Redius can't be negative. " +
                                            $"Current radius: {currentRadius} " +
                                            $"Radius step: {radiusStep}.");
        }
    }
}