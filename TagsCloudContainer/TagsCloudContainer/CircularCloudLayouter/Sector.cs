using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer.CircularCloudLayouter
{
    public class Sector
    {
        private readonly Point _center;
        private readonly List<Point> _points;
        private int _xCoefficient;
        private int _yCoefficient;

        public Sector(Quadrant quadrant, Point center)
        {
            _points = new List<Point>();
            _center = center;
            AddPoint(0, 0);

            InitCoefficients(quadrant);
        }

        private void InitCoefficients(Quadrant quadrant)
        {
            switch (quadrant)
            {
                case Quadrant.First:
                    _xCoefficient = 1;
                    _yCoefficient = 1;
                    break;
                case Quadrant.Second:
                    _xCoefficient = -1;
                    _yCoefficient = 1;
                    break;
                case Quadrant.Third:
                    _xCoefficient = -1;
                    _yCoefficient = -1;
                    break;
                case Quadrant.Fourth:
                    _xCoefficient = 1;
                    _yCoefficient = -1;
                    break;
            }
        }

        public Rectangle PlaceRectangle(double direction, Size rectangleSize)
        {
            var availablePoint = FindAvailablePoint(direction);
            RecalculatePointsInSector(availablePoint, rectangleSize);

            var rectangleLocation = FormatPoint(availablePoint, rectangleSize);

            return new Rectangle(rectangleLocation, rectangleSize);
        }

        private double MakeDirectionRelative(double direction)
        {
            return direction % (Math.PI / 2);
        }

        private Point FindAvailablePoint(double direction)
        {
            direction = MakeDirectionRelative(direction);
            for (var index = 0; index < PointsNumber(); index++)
            {
                var point = _points[index];
                if (IsPointOnDirectLine(point.X, point.Y, direction))
                    return point;

                if (IsPointUnderDirectLine(point.X, point.Y, direction))
                    return _points[index - 1].X == point.X ? point : _points[index - 1];
            }

            return new Point(0, 0);
        }

        public void RecalculatePointsInSector(Point downLeftPoint, Size rectangleSize)
        {
            var maxX = downLeftPoint.X + rectangleSize.Width;
            var maxY = downLeftPoint.Y + rectangleSize.Height;

            var rangeToRemove = FindPointsRangeToRemove(_points, maxX, maxY);
            var pointsToInsert = FindPointsToInsert(rectangleSize, _points, rangeToRemove, maxX, maxY);

            RemovePointsUnderNewRectangle(rangeToRemove);
            InsertNewPointsCreatedByRectangle(rangeToRemove.Item1, pointsToInsert);
        }

        public void InsertNewPointsCreatedByRectangle(int index, List<Point> pointsToInsert)
        {
            _points.InsertRange(index, pointsToInsert);
        }

        public static List<Point> FindPointsToInsert(Size rectangleSize,
            List<Point> points, Tuple<int, int> rangeToRemove, int maxX, int maxY)
        {
            var pointsToInsert = new List<Point>();
            pointsToInsert.InsertRange(0, HandleLeftBorder(rectangleSize, points, rangeToRemove, maxX, maxY));
            pointsToInsert.InsertRange(pointsToInsert.Count,
                HandleRightBorder(rectangleSize, points, rangeToRemove, maxX, maxY));

            return pointsToInsert;
        }

        public static List<Point> HandleLeftBorder(Size rectangleSize, List<Point> points,
            Tuple<int, int> rangeToRemove, int maxX, int maxY)
        {
            var pointsToInsert = new List<Point>();
            var leftIndex = rangeToRemove.Item1;

            if (leftIndex == 0)
            {
                pointsToInsert.Add(new Point(0, maxY));
            }
            else
            {
                var firstPointToDelete = points[leftIndex];
                pointsToInsert.Add(
                    new Point(firstPointToDelete.X, firstPointToDelete.Y + rectangleSize.Height));
            }

            pointsToInsert.Add(new Point(maxX, maxY));

            return pointsToInsert;
        }

        public static List<Point> HandleRightBorder(Size rectangleSize, List<Point> points,
            Tuple<int, int> rangeToRemove, int maxX, int maxY)
        {
            var pointsToInsert = new List<Point>();
            var leftIndex = rangeToRemove.Item1;
            var rightIndex = rangeToRemove.Item2;

            var pointsHadOneElement = leftIndex == rightIndex && leftIndex == 0;
            if (pointsHadOneElement || rightIndex == points.Count)
                pointsToInsert.Add(new Point(maxX, 0));
            else
                pointsToInsert.Add(new Point(maxX, points[rightIndex].Y));

            return pointsToInsert;
        }


        public static Tuple<int, int> FindPointsRangeToRemove(List<Point> points, int maxX, int maxY)
        {
            var rangeToRemove = new List<int>();

            for (var index = 0; index < points.Count; index++)
            {
                var point = points[index];
                if (point.X <= maxX && point.Y <= maxY)
                    rangeToRemove.Add(index);
            }

            return new Tuple<int, int>(rangeToRemove[0], rangeToRemove[rangeToRemove.Count - 1] + 1);
        }

        public void RemovePointsUnderNewRectangle(Tuple<int, int> rangeToRemove)
        {
            _points.RemoveRange(rangeToRemove.Item1, rangeToRemove.Item2 - rangeToRemove.Item1);
        }

        private Point FormatPoint(Point rectangleLocation, Size rectangleSize)
        {
            rectangleLocation = MakeCoordinatesAbsolute(rectangleLocation);
            rectangleLocation = FindUpperLeftPoint(rectangleLocation, rectangleSize);
            rectangleLocation = AddCenterCoordinatesToPoint(rectangleLocation);

            return rectangleLocation;
        }

        private Point FindUpperLeftPoint(Point firstToPlace, Size rectangleSize)
        {
            var deltaX = 0;
            var deltaY = 0;

            if (_xCoefficient == 1 && _yCoefficient == 1)
                deltaY = rectangleSize.Height;

            if (_xCoefficient == -1 && _yCoefficient == 1)
            {
                deltaX = -rectangleSize.Width;
                deltaY = rectangleSize.Height;
            }

            if (_xCoefficient == -1 && _yCoefficient == -1)
                deltaX = -rectangleSize.Width;

            var pointX = firstToPlace.X + deltaX;
            var pointY = firstToPlace.Y + deltaY;

            return new Point(pointX, pointY);
        }

        private Point MakeCoordinatesAbsolute(Point rectangleLocation)
        {
            rectangleLocation.X *= _xCoefficient;
            rectangleLocation.Y *= _yCoefficient;

            return rectangleLocation;
        }

        private Point AddCenterCoordinatesToPoint(Point rectangleLocation)
        {
            rectangleLocation.X += _center.X;
            rectangleLocation.Y += _center.Y;

            return rectangleLocation;
        }

        public static bool IsPointOnDirectLine(int x, int y, double direction)
        {
            return y == x * direction;
        }

        public static bool IsPointUnderDirectLine(int x, int y, double direction)
        {
            return y < x * direction;
        }

        private void AddPoint(int x, int y)
        {
            _points.Add(new Point(x, y));
        }

        private int PointsNumber()
        {
            return _points.Count;
        }
    }
}