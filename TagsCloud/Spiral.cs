using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloud
{
    public class Spiral
    {
        public List<Point> Points { get; set; }
        public List<Point> FreePoints { get; set; }
        public readonly Point center;
        private readonly double segmentLength;
        private readonly double helixPitch;
        private double lastX, lastY;

        public Spiral(Point center, double segmentLength = 50, double helixPitch = 20)
        {
            Points = new List<Point>();
            FreePoints = new List<Point>();
            this.center = center;
            this.segmentLength = segmentLength;
            this.helixPitch = helixPitch;

            double x = 0, y = 0;
            AddPoint(new Point(this.center.X, this.center.Y));
            y += segmentLength;
            AddPoint(new Point(this.center.X + (int)x, this.center.Y + (int)y));
            lastX = x;
            lastY = y;
        }

        public void AddPoint(Point pointToAdd)
        {
            Points.Add(pointToAdd);
            FreePoints.Add(pointToAdd);
        }

        public void ReleasePoint(Point pointToRemove)
        {
            FreePoints.Remove(pointToRemove);
        }

        public IEnumerable<Point> GetSpiralPoints()
        {
            foreach (var freePoint in FreePoints)
            {
                yield return freePoint;
            }

            // Точки из буффера закончились, вычисляем новые

            while (true)
            {
                double r = Math.Sqrt(lastX * lastX + lastY * lastY);
                double tx = helixPitch * lastX + r * lastY;
                double ty = helixPitch * lastY - r * lastX;
                double tLen = Math.Sqrt(tx * tx + ty * ty);
                double k = segmentLength / tLen;
                lastX -= tx * k;
                lastY -= ty * k;
                var newPoint = new Point((int)(center.X + lastX), (int)(center.Y + lastY));
                AddPoint(newPoint);
                yield return newPoint;
            }
        }
    }
}