using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagCloud
{
    public class TrianglePointsMaker : IPointsMaker
    {
        public IEnumerable<Point> GenerateNextPoint(Point center, double spiralStep)
        {
            yield return center;
            var distance = 5;
            while (true)
            {
                double x = center.X;
                double y = center.Y;
                for (; x < center.X + distance; x += spiralStep)
                    yield return new Point((int)x, (int)y);
                distance += 5;
                for (; x > center.X + distance * 0.5; x -= spiralStep * Math.Sin(Math.PI / 6))
                {
                    y += spiralStep * Math.Sin(Math.PI / 3);
                    yield return new Point((int)x, (int)y);
                }

                distance += 5;
                for (; x > center.X; x -= spiralStep * Math.Sin(Math.PI / 6))
                {
                    y -= spiralStep * Math.Sin(Math.PI / 3);
                    yield return new Point((int)x, (int)y);
                }    
                distance += 10;
            }
        }
    }
}