using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagCloud.PointGenerator
{
    public class Circle : IPointGenerator
    {
        private float spiralPitch;
        private readonly float anglePitch;
        private readonly double pitchCoefficient;
        public PointF Center { get; }
        private readonly ICache cache;

        public Circle(float anglePitch, double densityCoefficient, PointF center, ICache cache)
        {
            this.anglePitch = anglePitch;
            Center = center;
            this.cache = cache;
            pitchCoefficient = 20 * densityCoefficient * densityCoefficient;
        }

        public IEnumerable<PointF> GetPoints(SizeF size)
        {
            spiralPitch = (float)(Math.Min(size.Height, size.Width) / pitchCoefficient);
            foreach (var polar in ArchimedeanSpiral.GetArchimedeanSpiral(cache.SafeGetParameter(size),
                anglePitch, spiralPitch))
            {
                cache.UpdateParameter(size, polar.Angle);
                var cartesianPoint = polar.ToCartesian();
                yield return new PointF(cartesianPoint.X + Center.X, cartesianPoint.Y + Center.Y);
            }
        }

        public static Circle GetDefault()
        {
            return new Circle(0.1f, 0.9, new(0, 0), new Cache());
        }
    }
}