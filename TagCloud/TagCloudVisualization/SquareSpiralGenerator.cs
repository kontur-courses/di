using System.Collections.Generic;

namespace TagCloudVisualization
{
    public class SquareSpiralGenerator : AbstractSpiralGenerator
    {
        private protected override IEnumerator<Point> GetEnumerator()
        {
            Point currentPoint;
            yield return Center;
            yield return Center + Point.UnaryX;
            yield return currentPoint = Center + Point.UnaryX - Point.UnaryY;
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

            // ReSharper disable once IteratorNeverReturns
        }
    }
}
