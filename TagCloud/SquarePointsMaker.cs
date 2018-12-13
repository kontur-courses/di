using System.Collections.Generic;
using System.Drawing;

namespace TagCloud
{
    public class SquarePointsMaker : IPointsMaker
    {
        public IEnumerable<Point> GenerateNextPoint(Point center, double spiralStep)
        {
            yield return center;
            var distanse = 5;
            while (true)
            {
                double x = center.X;
                double y = center.Y;
                for (; x < center.X + distanse; x += spiralStep)
                    yield return new Point((int)x, (int)y);
                for (; y < center.Y + distanse; y += spiralStep)
                    yield return new Point((int)x, (int)y);
                for (; x > center.X; x -= spiralStep)
                    yield return new Point((int)x, (int)y);
                for (; y > center.Y; y -= spiralStep)
                    yield return new Point((int)x, (int)y);
                distanse += 20;
            }
        }
    }
}