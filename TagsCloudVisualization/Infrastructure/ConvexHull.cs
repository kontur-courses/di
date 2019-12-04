using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization.Infrastructure
{
    public class ConvexHull
    {
        public static List<Point> MakeHull(List<Point> points)
        {
            points.Sort(ComparePoints);
            if (points.Count <= 1)
                return new List<Point>(points);

            var upperHull = MakePartHull(points);

            var pointsReversed = new List<Point>(points);
            pointsReversed.Reverse();
            var lowerHull = MakePartHull(pointsReversed);

            if (!(upperHull.Count == 1 && upperHull.SequenceEqual(lowerHull)))
                upperHull.AddRange(lowerHull);
            return upperHull;
        }

        private static List<Point> MakePartHull(List<Point> points)
        {
            var hull = new List<Point>();
            foreach (var p in points)
            {
                while (hull.Count >= 2)
                {
                    var lastAdded = hull[hull.Count - 1];
                    var penultimateAdded = hull[hull.Count - 2];
                    if ((lastAdded.X - penultimateAdded.X) * (p.Y - penultimateAdded.Y) >=
                        (lastAdded.Y - penultimateAdded.Y) * (p.X - penultimateAdded.X))
                        hull.RemoveAt(hull.Count - 1);
                    else
                        break;
                }
                hull.Add(p);
            }
            hull.RemoveAt(hull.Count - 1);
            return hull;
        }

        private static int ComparePoints(Point point1, Point point2)
        {
            return point1.X < point2.X ? -1 :
                point1.X > point2.X ? +1 :
                point1.Y < point2.Y ? -1 :
                point1.Y > point2.Y ? +1 : 0;
        }
    }
}