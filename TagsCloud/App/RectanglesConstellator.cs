using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloud.App
{
    public class RectanglesConstellator : IRectanglesConstellator
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
        public int MaxX { get; private set; }
        public int MinX { get; private set; }
        public int MaxY { get; private set; }
        public int MinY { get; private set; }
        public int Width => MaxX - MinX;
        public int Height => MaxY - MinY;
        public readonly List<Rectangle> Rectangles;
        private readonly Dictionary<AngleDirection, Func<Point, Size, Rectangle>> directionToRectangle;
        private readonly Point center;
        private readonly List<Angle> angles;

        public RectanglesConstellator(Point center)
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
                MaxX = firstRectangle.Location.X + firstRectangle.Width;
                MinX = firstRectangle.Location.X;
                MaxY = firstRectangle.Location.Y + firstRectangle.Height;
                MinY = firstRectangle.Location.Y;
                return firstRectangle;
            }
            return AddNewRectangle(rectangleSize);
        }

        public void Clear()
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
            TryExpandImage(resultTuple.rectangle);
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

        private void TryExpandImage(Rectangle newRectangle)
        {
            if (newRectangle.Location.X + newRectangle.Width > MaxX)
                MaxX = newRectangle.Location.X + newRectangle.Width;
            if (newRectangle.Location.X < MinX)
                MinX = newRectangle.Location.X;
            if (newRectangle.Location.Y + newRectangle.Height > MaxY)
                MaxY = newRectangle.Location.Y + newRectangle.Height;
            if (newRectangle.Location.Y < MinY)
                MinY = newRectangle.Location.Y;
        }
    }
}
