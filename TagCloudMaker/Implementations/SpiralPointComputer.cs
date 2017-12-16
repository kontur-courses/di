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
        
        public Result<Point> GetNextPoint(double radiusStep, double angleStep)
        {
            if (currentRadius + radiusStep < 0)
                return Result.Fail<Point>($"Redius can't be negative. " +
                                            $"Current radius: {currentRadius} " +
                                            $"Radius step: {radiusStep}.");

            UpdateAngleAndRadius(radiusStep, angleStep);

            var x = center.X + (int)Math.Round(currentRadius * Math.Cos(currentAngle));
            var y = center.Y + (int)Math.Round(currentRadius * Math.Sin(currentAngle));
            return Result.Ok(new Point(x, y));
        }

        private void UpdateAngleAndRadius(double radiusStep, double angleStep)
        {
            var angleStepInRadians = angleStep * Math.PI / 360;
            currentAngle = (currentAngle + angleStepInRadians) % (Math.PI * 2);
            currentRadius += radiusStep;
        }
    }
}