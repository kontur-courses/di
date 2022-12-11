using System;
using System.Collections.Generic;
using System.Drawing;
using TagsCloud.Interfaces;

namespace TagsCloud
{
    public class Spiral : ISpiral
    {
        public List<Point> Points { get; set; }
        public List<Point> FreePoints { get; set; }
        public Point Center { get; set; }

        private readonly double segmentLength;
        private readonly double helixPitch;
        private double lastX, lastY;

        public Spiral(Point center, double segmentLength = 50, double helixPitch = 20)
        {
            Points = new List<Point>();
            FreePoints = new List<Point>();
            Center = center;
            this.segmentLength = segmentLength;
            this.helixPitch = helixPitch;

            double x = 0, y = 0;
            AddPoint(new Point(Center.X, Center.Y));
            y += segmentLength;
            AddPoint(new Point(Center.X + (int)x, Center.Y + (int)y));
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
                var newPoint = new Point((int)(Center.X + lastX), (int)(Center.Y + lastY));
                AddPoint(newPoint);
                yield return newPoint;
            }
        }
    }
}