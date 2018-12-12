using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TagsCloudContainer.Extensions;
using TagsCloudContainer.Settings;

namespace TagsCloudContainer.CircularCloudLayouters
{
    public class CircularPointsChooser : IEnumerator<Point>
    {
        private readonly Point centerPoint;
        private IEnumerator<Point> pointsOrder;

        public CircularPointsChooser(IImageSettings imageSettings)
        {
            var size = imageSettings.ImageSize;
            centerPoint = new Point(size.Width / 2, size.Height / 2);;
            ResetEnumerator();
        }

        public void Dispose()
        {
            pointsOrder.Dispose();
        }

        public bool MoveNext()
            => pointsOrder.MoveNext();

        public void Reset()
        {
            ResetEnumerator();
        }

        public Point Current => pointsOrder.Current;

        object IEnumerator.Current => pointsOrder.Current;

        private void ResetEnumerator()
        {
            pointsOrder = GetCircularPoints().GetEnumerator();
        }

        private IEnumerable<Point> GetCircularPoints()
        {
            yield return centerPoint;
            var radius = 1;
            while (true)
            {
                foreach (var point in GetPointsAtCircle(radius))
                    yield return point;
                radius++;
            }
        }

        private IEnumerable<Point> GetPointsAtCircle(int radius)
        {
            for (var x = -radius; x <= radius; x++)
            {
                var y = (int)Math.Sqrt(radius * radius - x * x);
                yield return centerPoint.Shift(x, y);
                if (y > 0)
                    yield return centerPoint.Shift(x, -y);
            }
        }
    }
}