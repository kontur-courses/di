using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization
{
    public static class ConvexHullBuilder
    {
        /// <summary>
        /// Returns the integer that indicates point location relative to the vector.
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="point"></param>
        /// <returns>A number that indicates <paramref name="point"/> location relative to the
        /// <paramref name="vector"/>.
        /// Return value meaning: -1 <paramref name="point"/> is located to the right
        /// of the <paramref name="vector"/>.
        /// 0 <paramref name="point"/> is located on the <paramref name="vector"/>.
        /// 1 <paramref name="point"/> is located to the left of the <paramref name="vector"/>.
        /// </returns>
        public static int GetRotationDirection(Vector vector, Point point)
        {
            var vectorProduct = (vector.End.X - vector.Begin.X) * (point.Y - vector.Begin.Y) 
                                - (vector.End.Y - vector.Begin.Y) * (point.X - vector.Begin.X);
            return Math.Sign(vectorProduct);
        }

        public static IReadOnlyCollection<Point> GetRectanglesPointsSet(
            IReadOnlyCollection<Rectangle> rectangles)
        {
            return rectangles
                .SelectMany(rect => new List<Point>
                {
                    new Point(rect.Left, rect.Bottom),
                    new Point(rect.Right, rect.Bottom),
                    new Point(rect.Left, rect.Top),
                    new Point(rect.Right, rect.Top)
                })
                .Distinct()
                .ToList();
        }

        public static IReadOnlyCollection<Point> GetConvexHull(IReadOnlyCollection<Point> points)
        {
            return points.Count <= 3 ? points : GetConvexHullByJarvisAlgorithm(points);
        }

        private static IReadOnlyCollection<Point> GetConvexHullByJarvisAlgorithm(
            IReadOnlyCollection<Point> points)
        {
            var convexHull = new List<Point>();
            var leftMostPoint = GetLeftMostPoint(points);
            var lastAddedPoint = leftMostPoint;
            Point hullCandidate;
            do
            {
                hullCandidate = GetBestCandidate(points, lastAddedPoint);
                convexHull.Add(hullCandidate);
                lastAddedPoint = hullCandidate;
            } while (hullCandidate != leftMostPoint);

            return convexHull;
        }

        private static Point GetBestCandidate(IReadOnlyCollection<Point> points, Point lastAddedPoint)
        {
            var hullCandidate = GetAnyCandidateExceptLastAdded(points, lastAddedPoint);
            var candidateVector = new Vector(lastAddedPoint, hullCandidate);
            foreach (var point in points)
                if (GetRotationDirection(candidateVector, point) < 0)
                {
                    hullCandidate = point;
                    candidateVector = new Vector(lastAddedPoint, hullCandidate);
                }

            return hullCandidate;
        }

        private static Point GetAnyCandidateExceptLastAdded(IReadOnlyCollection<Point> points, Point lastAdded)
        {
            return points.First(p => p != lastAdded);
        }

        private static Point GetLeftMostPoint(IReadOnlyCollection<Point> points)
        {
            return points.OrderBy(p => p.X).First();
        }
    }

    public static class CircularCloudLayouterExtensions
    {
        public static IReadOnlyCollection<Point> GetCloudConvexHull(this CircularCloudLayouter layouter)
        {
            var rectanglesPoints = ConvexHullBuilder.GetRectanglesPointsSet(layouter.Rectangles);
            var cloudConvexHull = ConvexHullBuilder.GetConvexHull(rectanglesPoints);
            return cloudConvexHull;
        }
    }
}
