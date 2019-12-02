using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization
{
    public class PointRadiusComparer : IComparer<Point>
    {
        public int Compare(Point firstPoint, Point secondPoint) =>
            GetRadius(firstPoint).CompareTo(GetRadius(secondPoint));

        private double GetRadius(Point point) => point.X * point.X + point.Y * point.Y;
    }
}