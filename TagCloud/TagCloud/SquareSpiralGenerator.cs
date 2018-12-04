using System.Collections;
using System.Collections.Generic;

namespace TagsCloudVisualization
{
    public class SquareSpiralGenerator : IEnumerable<Point>, ISpiralGenerator
    {
        private readonly Point _center;

        public SquareSpiralGenerator(Point center)
        {
            this._center = center;
        }

        public IEnumerator<Point> GetEnumerator()
        {
            Point currentPoint;
            yield return _center;
            yield return _center + Point.UnaryX;
            yield return currentPoint = _center + Point.UnaryX - Point.UnaryY;
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
