using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudVisualization.Interfaces;

namespace TagsCloudVisualization
{
    public class CircularCloudLayouter : ICloudLayouter
    {
        private readonly List<Rectangle> rectangles = new();
        private readonly Point center;
        private int angle;
        private readonly int step;

        public CircularCloudLayouter(Point center, int step = 10)
        {
            this.center = center;
            this.step = step;
            angle = 0;
        }

        public Rectangle[] GetPutRectangles()
        {
            return rectangles.ToArray();
        }

        public Point GetCenter()
        {
            return center;
        }

        public Rectangle PutNextRectangle(string word, int fontSize)
        {
            return PutNextRectangle(new Size(word.Length * fontSize, (int)(fontSize * 1.3)));
        }

        public IEnumerable<Rectangle> PutRectangles(IEnumerable<string> words, int fontSize)
        {
            return words.Select(word => PutNextRectangle(word, fontSize));
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            if (rectangleSize.Height <= 0 || rectangleSize.Width <= 0)
                throw new ArgumentException(
                    $"Size is empty! Width: {rectangleSize.Width}, height: {rectangleSize.Height}");

            var nextRectangle = MoveToCenter(GetNextPossibleRectangle(rectangleSize));

            rectangles.Add(nextRectangle);
            return nextRectangle;
        }

        private Point GetNextPointAndUpdateAngle()
        {
            var length = step / (2 * Math.PI) * angle * Math.PI / 180;
            var x = (int)(length * Math.Cos(angle)) + center.X;
            var y = (int)(length * Math.Sin(angle)) + center.Y;
            angle++;

            return new Point(x, y);
        }

        private bool DoesIntersectPreviousRectangles(Rectangle rectangle)
        {
            return rectangles.Any(x => x.IntersectsWith(rectangle));
        }

        private double GetDistanceFromCenter(Point? point)
        {
            if (!point.HasValue)
                throw new ArgumentException("Point was null");

            var value = point.Value;
            return Math.Sqrt(Math.Pow(center.X - value.X, 2) + Math.Pow(center.Y - value.Y, 2));
        }

        private Rectangle MoveToCenter(Rectangle previousRectangle)
        {
            var newRectangle = previousRectangle;

            while (true)
            {
                var maxPossibleDistanceUp = int.MaxValue;
                var maxPossibleDistanceDown = int.MaxValue;
                var maxPossibleDistanceRight = int.MaxValue;
                var maxPossibleDistanceLeft = int.MaxValue;

                foreach (var rect in rectangles)
                {
                    if (DoesSegmentsIntersect(rect.Left, rect.Right,
                        newRectangle.Left, newRectangle.Right))
                    {
                        if (rect.Bottom <= newRectangle.Top)
                            maxPossibleDistanceUp =
                                Math.Min(newRectangle.Top - rect.Bottom, maxPossibleDistanceUp);
                        else if (rect.Top >= newRectangle.Bottom)
                            maxPossibleDistanceDown =
                                Math.Min(rect.Top - newRectangle.Bottom, maxPossibleDistanceDown);
                    }

                    if (DoesSegmentsIntersect(rect.Top, rect.Bottom,
                        newRectangle.Top, newRectangle.Bottom))
                    {
                        if (rect.Right <= newRectangle.Left)
                            maxPossibleDistanceLeft =
                                Math.Min(newRectangle.Left - rect.Right, maxPossibleDistanceLeft);
                        else if (rect.Left >= newRectangle.Right)
                            maxPossibleDistanceRight = Math.Min(rect.Left - newRectangle.Right,
                                maxPossibleDistanceRight);
                    }
                }

                Point? newPoint = null;

                if (CanMadeMoveOnDistance(maxPossibleDistanceDown))
                    newPoint = new Point(newRectangle.X, newRectangle.Y + maxPossibleDistanceDown);

                else if (CanMadeMoveOnDistance(maxPossibleDistanceUp))
                    newPoint = new Point(newRectangle.X, newRectangle.Y - maxPossibleDistanceUp);

                else if (CanMadeMoveOnDistance(maxPossibleDistanceLeft))
                    newPoint = new Point(newRectangle.X - maxPossibleDistanceLeft, newRectangle.Y);

                else if (CanMadeMoveOnDistance(maxPossibleDistanceRight))
                    newPoint = new Point(newRectangle.X + maxPossibleDistanceRight, newRectangle.Y);

                if (newPoint == null || GetDistanceFromCenter(newPoint) >= GetDistanceFromCenter(newRectangle.Location))
                    break;

                newRectangle.Location = newPoint.Value;
            }

            return newRectangle;
        }

        private static bool CanMadeMoveOnDistance(int distance)
        {
            return distance > 0 && distance != int.MaxValue;
        }

        private Rectangle GetNextPossibleRectangle(Size size)
        {
            Rectangle nextRectangle;

            do
            {
                var nextPoint = GetNextPointAndUpdateAngle();
                nextRectangle = new Rectangle(nextPoint, size);
            } while (DoesIntersectPreviousRectangles(nextRectangle));

            return nextRectangle;
        }

        private static bool DoesSegmentsIntersect(int firstSegmentStart, int firstSegmentEnd, int secondSegmentStart,
            int secondSegmentEnd)
        {
            return Math.Max(firstSegmentStart, secondSegmentStart) < Math.Min(firstSegmentEnd, secondSegmentEnd);
        }
    }
}