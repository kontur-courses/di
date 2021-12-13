using System;
using System.Collections.Generic;

namespace TagCloud.PointGenerator
{
    public class ArchimedeanSpiral
    {
        public static IEnumerable<PolarCoordinates> GetArchimedeanSpiral(float startAngle, float spiralPitch,
            float anglePitch)
        {
            while (true)
            {
                var radius = (float)(spiralPitch * startAngle / (2 * Math.PI));
                yield return new PolarCoordinates(radius, startAngle);
                startAngle += anglePitch;
            }
            // ReSharper disable once IteratorNeverReturns
        }
    }
}