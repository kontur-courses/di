using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudVisualization.Extensions;

namespace TagsCloudVisualization
{
    public class CircularCloudMaker : ICloudMaker
    {
        private const double FloatDelta = 0.01;
        
        public readonly PointF Center;
        private readonly List<RectangleF> rectangles = new List<RectangleF>();
        private readonly HashSet<PointF> placementLocations = new HashSet<PointF>();
        private readonly Func<PointF, PointF, double> distanceFunction;

        public IReadOnlyList<RectangleF> Rectangles => rectangles;
        public double Radius => placementLocations.Max(p => GetDistanceBetween(p, Center));
        
        public CircularCloudMaker(Point center) : this(center, Distance) {}

        public CircularCloudMaker(Point center, Func<PointF, PointF, double> distanceFunction)
        {
            Center = center;
            this.distanceFunction = distanceFunction;
        }

        public RectangleF PutRectangle(Size rectangleSize)
        {
            return rectangles.Count == 0 ? PutFirstRectangle(rectangleSize) : PutNextRectangle(rectangleSize);
        }

        private RectangleF PutFirstRectangle(Size rectangleSize)
        {
            var firstRectangle = CreateRectangle(Center, rectangleSize);
            AddRectanglePointsToPlacementLocations(firstRectangle, Center);
            return firstRectangle;
        }

        private RectangleF PutNextRectangle(Size rectangleSize)
        {
            var (rectangle, placementLocation) = placementLocations
                .Select(p => (rectangleCenter: GetRectangleCenter(p, rectangleSize), placementLocation: p))
                .OrderBy(p => GetDistanceBetween(Center, p.rectangleCenter))
                .Select(points => (rectangle: CreateRectangle(points.rectangleCenter, rectangleSize),
                    points.placementLocation))
                .First(x => RectangleCanBePlaced(x.rectangle, x.placementLocation));
            
            AddRectanglePointsToPlacementLocations(rectangle, placementLocation);
            return rectangle;
        }

        private void AddRectanglePointsToPlacementLocations(RectangleF rectangle, PointF placement)
        {
            var points = GetPoints(rectangle);
            points.Remove(placement);
            placementLocations.Remove(placement);
            rectangles.Add(rectangle);
            foreach (var point in points)
            {
                placementLocations.Add(point);
                placementLocations.Add(new PointF(point.X, Center.Y));
                placementLocations.Add(new PointF(Center.X, point.Y));
            }
        }

        private PointF GetRectangleCenter(PointF placement, Size rectangleSize)
        {
            var offsetX = 0f;
            var offsetY = 0f;
            if (Math.Abs(placement.X - Center.X) > FloatDelta)
                offsetX = Math.Sign(placement.X - Center.X) * rectangleSize.Width / 2.0f;
            if (Math.Abs(placement.Y - Center.Y) > FloatDelta )
                offsetY = Math.Sign(placement.Y - Center.Y) * rectangleSize.Height / 2.0f;
            return new PointF(placement.X + offsetX, placement.Y + offsetY);
        }

        private bool RectangleCanBePlaced(RectangleF rectangle, PointF placement)
        {
            var canBePlaced = rectangles
                .All(r => !r.IntersectsWith(rectangle));
            if (canBePlaced)
                return true;
            var minSize = new Size(1, 1);
            var possibleRectangleCenter = GetRectangleCenter(placement, minSize);
            var possibleRect = CreateRectangle(possibleRectangleCenter, minSize);
            if (rectangles.Any(r => r.IntersectsWith(possibleRect)))
                placementLocations.Remove(placement);
            return false;
        }

        private static RectangleF CreateRectangle(PointF center, Size size)
        {
            return new RectangleF(center.X - size.Width / 2.0f, center.Y - size.Height / 2.0f, size.Width, size.Height);
        }
        
        private  double GetDistanceBetween(PointF point, PointF other)
        {
            return distanceFunction(point, other);
        }
        
        private static List<PointF> GetPoints(RectangleF rectangle)
        {
            return new List<PointF>()
            {
                rectangle.Location,
                new PointF(rectangle.Right, rectangle.Top),
                new PointF(rectangle.Right, rectangle.Bottom),
                new PointF(rectangle.Left, rectangle.Bottom)
            };
        }

        public static double ManhattanDistance(PointF point, PointF other)
        {
            return Math.Abs(point.X - other.X) + Math.Abs(point.Y - other.Y);
        }
        
        public static double Distance(PointF point, PointF other)
        {
            return point.DistanceTo(other);
        }
    }
}