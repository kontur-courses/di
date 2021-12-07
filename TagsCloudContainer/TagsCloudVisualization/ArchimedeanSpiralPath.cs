using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization
{
    public class ArchimedeanSpiralPath : IEnumerable<Point>
    {
        public int Degree { get; private set; }

        private readonly ArchimedeanSpiral spiral;
        private readonly int step;
        private Point lastPoint;
        private bool isFirstPoint = true;

        public ArchimedeanSpiralPath(ArchimedeanSpiral spiral, int step = 1)
        {
            if (step < 0)
                throw new ArgumentException("Step can't be negative.", nameof(step));

            this.spiral = spiral ?? throw new ArgumentException("Spiral refers to null.", nameof(spiral));
            this.step = step;
        }

        public IEnumerator<Point> GetEnumerator() => GetPoints().GetEnumerator();

        public Point GetNextPoint()
        {
            while (true)
            {
                var point = spiral.GetPoint(Degree);
                Degree += step;
                if (!isFirstPoint && point.Equals(lastPoint))
                    continue;

                lastPoint = point;
                isFirstPoint = false;
                return point;
            }
        }

        public void Reset()
        {
            Degree = 0;
            isFirstPoint = true;
            lastPoint = new Point();
        }

        private IEnumerable<Point> GetPoints()
        {
            while (true)
                yield return GetNextPoint();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}