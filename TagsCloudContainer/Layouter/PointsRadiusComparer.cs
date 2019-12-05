using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TagsCloudContainer.Layouter
{
    public class PointsRadiusComparer : IComparer<Point>
    {
        private readonly Point center;

        public PointsRadiusComparer(Point center)
        {
            this.center = center;
        }

        public int Compare(Point firstPoint, Point secondPoint)
        {
            var distancesDifference = firstPoint.SquaredDistanceTo(center) - secondPoint.SquaredDistanceTo(center);
            if (distancesDifference != 0)
                return distancesDifference;
            var xDifference = firstPoint.X - secondPoint.X;
            if (xDifference != 0)
                return xDifference;
            return firstPoint.Y - secondPoint.Y;
        }
    }
}
