using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EParser;
using OptimizationMethods;

namespace TagsCloudVisualization
{
    public class CircularCloudLayouter
    {
        private readonly List<Rectangle> _rectangles = new List<Rectangle>();
        private readonly HashSet<Point> _startPoints = new HashSet<Point>();
        private readonly List<Func> _rectanglePenaltyFunctions = new List<Func>();
        private Point _center;
        private Func _distanceFunction;

        public Point Center => _center;
        public IReadOnlyList<Rectangle> Rectangles => _rectangles.AsReadOnly();

        public CircularCloudLayouter(Point center)
        {
            _center = center;
            _distanceFunction = t => (t[0] - _center.X) * (t[0] - _center.X) +
                                     (t[1] - _center.Y) * (t[1] - _center.Y);
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            if (rectangleSize.IsEmpty)
                throw new ArgumentException($"{nameof(rectangleSize)} is empty");

            UpdateRectanglePenaltyFunctions(rectangleSize);

            Rectangle bestRectangle = GenerateRectangle(rectangleSize);

            _rectangles.Add(bestRectangle);
            AddRectanglePointsToStartPoints(bestRectangle);
            
            return bestRectangle;
        }

        private void UpdateRectanglePenaltyFunctions(Size targetRectangleSize)
        {
            _rectanglePenaltyFunctions.Clear();

            Point location;
            Size size;
            foreach (var rectangle in _rectangles)
            {
                location = new Point(rectangle.X - targetRectangleSize.Width / 2,
                                     rectangle.Y - targetRectangleSize.Height / 2);
                size = new Size(rectangle.Width + targetRectangleSize.Width,
                                rectangle.Height + targetRectangleSize.Height);
                _rectanglePenaltyFunctions.Add(BuildRectanglePenaltyFunction(new Rectangle(location, size)));
            }
        }

        private Rectangle GenerateRectangle(Size requiredRectangleSize)
        {
            if (!_rectangles.Any())
                return BuildRectangle(_center, requiredRectangleSize);

            double minFunctionValue = double.MaxValue, currentFunctionValue;
            Rectangle bestRectangle = default, currentRectangle;
            Point currentRectangleCenter;
            foreach (var startPoint in _startPoints)
            {
                currentRectangleCenter = PenaltyMethodFacade.Calculate(_distanceFunction, startPoint, 
                                                                       _rectanglePenaltyFunctions);
                currentFunctionValue = _distanceFunction(currentRectangleCenter.X, currentRectangleCenter.Y) +
                                       _rectanglePenaltyFunctions
                                            .Select(f => f(currentRectangleCenter.X, currentRectangleCenter.Y))
                                            .Sum();
                if (currentFunctionValue < minFunctionValue)
                {
                    currentRectangle = BuildRectangle(currentRectangleCenter, requiredRectangleSize);
                    if (_rectangles.Any(rectangle => rectangle.IntersectsWith(currentRectangle)))
                        continue;
                    bestRectangle = currentRectangle;
                    minFunctionValue = currentFunctionValue;
                }
            }

            if (minFunctionValue == double.MaxValue)
                throw new Exception("Can't find optimal point for new rectangle");

            return bestRectangle;
        }

        private void AddRectanglePointsToStartPoints(Rectangle rectangle)
        {
            int halfWidth = rectangle.Width / 2;
            int halfHeight = rectangle.Height / 2;

            _startPoints.Add(rectangle.Location);
            _startPoints.Add(new Point(rectangle.X + rectangle.Width, rectangle.Y));
            _startPoints.Add(new Point(rectangle.X, rectangle.Y + rectangle.Height));
            _startPoints.Add(new Point(rectangle.X + rectangle.Width, rectangle.Y + rectangle.Height));

            _startPoints.Add(new Point(rectangle.X + halfWidth, rectangle.Y));
            _startPoints.Add(new Point(rectangle.X, rectangle.Y + halfHeight));
            _startPoints.Add(new Point(rectangle.X + rectangle.Width, rectangle.Y + halfHeight));
            _startPoints.Add(new Point(rectangle.X + halfWidth, rectangle.Y + rectangle.Height));
        }

        private static Rectangle BuildRectangle(Point center, Size size)
        {
            var location = new Point(center.X - size.Width / 2, center.Y - size.Height / 2);
            return new Rectangle(location, size);
        }

        private static Func BuildRectanglePenaltyFunction(Rectangle rectangle)
        {
            int left = rectangle.Left;
            int right = rectangle.Right;
            int bottom = rectangle.Bottom;
            int top = rectangle.Top;

            return t =>
            {
                double maxX = Math.Max(left - t[0], t[0] - right);
                double maxY = Math.Max(top - t[1], t[1] - bottom);
                double penalty = Math.Min(0, Math.Max(maxX, maxY));
                if (penalty < 0)
                    penalty *= -1E+15;
                return penalty;
            };
        }
    }
}