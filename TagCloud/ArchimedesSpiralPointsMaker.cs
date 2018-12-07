using System;
using System.Collections.Generic;
using System.Drawing;


namespace TagsCloudVisualization
{
    static class ArchimedesSpiralPointsMaker
    {

        public static IEnumerable<Point> GenerateNextPoint(Point center, double spiralStep)
        {
            yield return center;
            var angle = 0.0;
            const double angleDelta = Math.PI / 90;
            while (true)
            {
                angle += angleDelta;
                var distance = spiralStep * angle;
                var x = distance * Math.Sin(angle);
                var y = distance * Math.Cos(angle);
                yield return new Point((int)x + center.X, (int)y + center.Y);
            }
            
        }
    }
}
