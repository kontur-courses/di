using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace WindowsFormsApp1
{
    public class ArchimedeanCircularCloudLayouter : ICircularCloudLayouter
    {
        private Point center;
        private List<Rectangle> cloudRectangles;
        private ArchimedeanSpiral archimedeanSpiral;
        public ArchimedeanCircularCloudLayouter(Point center)
        {
            this.center = center;
            cloudRectangles = new List<Rectangle>();
            archimedeanSpiral = new ArchimedeanSpiral(center, 0.5);
        }

        public Rectangle PutNextRectangle(Size size)
        {
            var w = size.Width;
            var h = size.Height;
            if (w <= 0 || h <= 0)
                throw new ArgumentException();
            foreach (var point in archimedeanSpiral)
            {
                var rect = new Rectangle(new Point(point.X - w / 2, point.Y - h / 2), size);
                if (cloudRectangles.Any(x => x.IntersectsWith(rect))) continue;
                cloudRectangles.Add(rect);
                return rect;
            }
            return Rectangle.Empty;
        }
        private class ArchimedeanSpiral : IEnumerable<Point>
        {
            private Point center;
            private readonly double removalRatio;
            public ArchimedeanSpiral(Point center, double removalRatio)
            {
                this.center = center;
                this.removalRatio = removalRatio;
            }
            public IEnumerator<Point> GetEnumerator()
            {
                var angleInDegrees = 0;
                while (true)
                {
                    var angleInRadians = angleInDegrees * Math.PI / 180;
                    var radius = removalRatio * angleInRadians;
                    yield return new Point(
                        x: (int)(center.X + radius * Math.Cos(angleInRadians)),
                        y: (int)(center.Y + radius * Math.Sin(angleInRadians)));
                    angleInDegrees++;
                }
            }
            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
    }
}