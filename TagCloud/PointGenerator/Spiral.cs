using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagCloud.PointGenerator
{
    public class Spiral : IPointGenerator
    {
        private double spiralPitch;
        private readonly float anglePitch;
        private readonly double pitchCoefficient;
        public PointF Center { get; }
        private readonly ICache cache;

        public Spiral(float anglePitch, double densityCoefficient, PointF center, ICache cache)
        {
            this.anglePitch = anglePitch;
            Center = center;
            this.cache = cache;
            pitchCoefficient = 20 * densityCoefficient * densityCoefficient;
        }

        public IEnumerable<PointF> GetPoints(Size size)
        {
            spiralPitch = Math.Min(size.Height, size.Width)/pitchCoefficient;
            foreach (var (radius, angle) in GetArchimedeanSpiral(cache.SafeGetParameter(size)))
            {
                cache.UpdateParameter(size, angle);
                var (x, y) = PolarToCartesian(radius, angle);
                yield return new PointF(x + Center.X, y + Center.Y);
            }
        }
        
        private IEnumerable<(float radius, float angle)> GetArchimedeanSpiral(float currentAngle)
        {
            while (true)
            {
                var radius = (float)(spiralPitch * currentAngle / (2 * Math.PI));
                yield return (radius, currentAngle);
                currentAngle += anglePitch;
            }
            // ReSharper disable once IteratorNeverReturns
        }

        private static (float x, float y) PolarToCartesian(float r, float angle)
        {
            var x = (float)(r * Math.Cos(angle));
            var y = (float)(r * Math.Sin(angle));
            return (x, y);
        }
    }
}