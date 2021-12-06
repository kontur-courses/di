using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualizationTests
{
    internal static class Generators
    {
        public static IEnumerable<Point> CenterGenerator()
        {
            yield return new Point(0, 0);
            yield return new Point(-10, -10);
            yield return new Point(10, 10);
            yield return new Point(-10, 10);
            yield return new Point(10, -10);
            yield return new Point(0, 10);
            yield return new Point(0, -10);
            yield return new Point(10, 0);
            yield return new Point(-10, 0);
        }

        public static IEnumerable<SizeF> RectanglesRandomSizeGenerator(int rectanglesCount)
        {
            var random = new Random();

            for (var i = 0; i < rectanglesCount; ++i)
            {
                yield return new SizeF(
                    (float) 0.3 * random.Next(1, 100),
                    (float) 0.3 * random.Next(1, 100));
            }
        }
    }
}