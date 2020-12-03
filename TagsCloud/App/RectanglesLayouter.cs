using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloud.App
{
    public class RectanglesLayouter : IRectanglesLayouter
    {
        private enum AngleDirection
        {
            LeftBottom,
            RightBottom,
            LeftTop,
            RightTop
        }

        private class Angle
        {
            public Point Pos;
            public AngleDirection Direction;
        }

        public string Name { get; } = "По умолчанию";
        public readonly List<Rectangle> Rectangles;
        private readonly Dictionary<AngleDirection, Func<Point, Size, Rectangle>> directionToRectangle;
        private readonly Point center;
        private readonly List<Angle> angles;

        public RectanglesLayouter(Point center)
        {
            this.center = center;
            Rectangles = new List<Rectangle>();
            angles = new List<Angle>();
            directionToRectangle = new Dictionary<AngleDirection, Func<Point, Size, Rectangle>>
            {
                {
                    AngleDirection.RightBottom, (point, size) =>
                        new Rectangle(point, size)
                },
                {
                    AngleDirection.LeftBottom, (point, size) =>
                        new Rectangle(new Point(point.X - size.Width, point.Y), size)
                },
                {
                    AngleDirection.RightTop, (point, size) =>
                        new Rectangle(new Point(point.X, point.Y - size.Height), size)
                },
                {
                    AngleDirection.LeftTop, (point, size) =>
                        new Rectangle(point - size, size)
                }
            };
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            if (Rectangles.Count == 0)
            {
                var firstRectangle = new Rectangle(center.X - rectangleSize.Width / 2,
                    center.Y - rectangleSize.Height / 2,
                    rectangleSize.Width, rectangleSize.Height);
                Rectangles.Add(firstRectangle);
                AddAngles(Rectangles[0]);
                return firstRectangle;
            }
            return AddNewRectangle(rectangleSize);
        }

        public void Reset()
        {
            Rectangles.Clear();
            angles.Clear();
        }

        public static Point CalculateCenterPosition(Rectangle rectangle)
        {
            return rectangle.Location + rectangle.Size / 2;
        }

        public static double CalculateDistance(Point point1, Point point2)
        {
            return Math.Sqrt(Math.Pow(point1.X - point2.X, 2) + Math.Pow(point1.Y - point2.Y, 2));
        }

        private Rectangle AddNewRectangle(Size rectangleSize)
        {
            var resultTuple = angles
                .Select(angle => (rectangle: directionToRectangle[angle.Direction](angle.Pos, rectangleSize), angle))
                .Where(tuple => !Rectangles.Any(anotherRectangle => anotherRectangle.IntersectsWith(tuple.Item1)))
                .OrderBy(tuple =>
                    CalculateDistance(center, CalculateCenterPosition(tuple.rectangle)))
                .First();
            Rectangles.Add(resultTuple.rectangle);
            angles.Remove(resultTuple.angle);
            AddAngles(resultTuple.rectangle);
            return resultTuple.rectangle;
        }

        private void AddAngles(Rectangle rect)
        {
            angles.Add(new Angle { Direction = AngleDirection.LeftBottom, Pos = rect.Location });
            angles.Add(new Angle { Direction = AngleDirection.RightTop, Pos = rect.Location });
            angles.Add(new Angle { Direction = AngleDirection.LeftBottom, Pos = rect.Location + rect.Size });
            angles.Add(new Angle { Direction = AngleDirection.RightTop, Pos = rect.Location + rect.Size });
            angles.Add(new Angle { Direction = AngleDirection.RightBottom, Pos = rect.Location + new Size(rect.Size.Width, 0) });
            angles.Add(new Angle { Direction = AngleDirection.LeftTop, Pos = rect.Location + new Size(rect.Size.Width, 0) });
            angles.Add(new Angle { Direction = AngleDirection.RightBottom, Pos = rect.Location + new Size(0, rect.Size.Height) });
            angles.Add(new Angle { Direction = AngleDirection.LeftTop, Pos = rect.Location + new Size(0, rect.Size.Height) });
        }
    }
}
