using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using App.Implementation.GeometryUtils;
using App.Infrastructure.LayoutingAlgorithms.AlgorithmFromTDD;

namespace App.Implementation.LayoutingAlgorithms.AlgorithmFromTDD
{
    public class CircularLayouter : ICloudLayouter
    {
        private readonly DirectingArrow arrow;
        private readonly List<Rectangle> rectangles;

        public CircularLayouter(Point center = default)
        {
            Center = center;
            rectangles = new List<Rectangle>();
            arrow = new DirectingArrow(Center);
        }

        public Point Center { get; }

        public List<Rectangle> GetRectanglesCopy()
        {
            return new List<Rectangle>(rectangles);
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            ThrowIfIncorrectSize(rectangleSize);

            var rect = new Rectangle(Center.MovePointToSizeCenter(rectangleSize, false), rectangleSize);

            while (rect.IntersectsWithAny(rectangles))
                rect = new Rectangle(arrow.Rotate().MovePointToSizeCenter(rect.Size, false), rect.Size);

            rectangles.Add(rect);
            return rect;
        }

        public int GetCloudBoundaryRadius()
        {
            return rectangles.Count == 0
                ? 0
                : (int)Math.Ceiling(rectangles
                    .SelectMany(rect => rect
                        .GetCorners())
                    .Max(corner => corner.GetDistanceTo(Center)));
        }

        public Size GetRectanglesBoundaryBox()
        {
            if (rectangles.Count == 0)
                return Size.Empty;

            var width
                = rectangles.Max(rect => rect.Right) - rectangles.Min(rect => rect.X);
            var height
                = rectangles.Max(rect => rect.Bottom) - rectangles.Min(rect => rect.Y);

            return new Size(width, height);
        }

        private void ThrowIfIncorrectSize(Size rectangleSize)
        {
            if (rectangleSize.Width <= 0 || rectangleSize.Height <= 0)
                throw new ArgumentException("Width and height of rectangle must be a positive numbers");
        }
    }
}