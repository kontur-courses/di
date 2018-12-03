using System.Collections;
using System.Collections.Generic;

namespace TagsCloudVisualization
{
    internal class SquareSpiralGenerator : IEnumerable<Point>
    {
        private readonly Point center;

        public SquareSpiralGenerator(Point center)
        {
            this.center = center;
        }

        public IEnumerator<Point> GetEnumerator()
        {
            Point currentPoint;
            yield return center;
            yield return center + Point.UnaryX;
            yield return currentPoint = center + Point.UnaryX - Point.UnaryY;
            var iteration = 1;
            while (true)
            {
                var sign = iteration % 2 == 1 ? 1 : -1;
                for (var i = 0; i < iteration; i++)
                    yield return currentPoint = currentPoint - sign * Point.UnaryX;

                for (var i = 0; i < iteration; i++)
                    yield return currentPoint = currentPoint + sign * Point.UnaryY;

                iteration++;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
