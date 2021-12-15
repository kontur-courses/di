using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagCloud.Extensions;

namespace TagCloud.Layout
{
    internal class CircularCloudLayouter : ICloudLayouter
    {
        private readonly ICurve _curve;
        private readonly List<Rectangle> _rectangles = new();
        public readonly Point Center;

        public CircularCloudLayouter(ICurve curve) : this(Point.Empty, curve)
        {
        }

        public CircularCloudLayouter(Point center, ICurve curve)
        {
            Center = center;
            _curve = curve;
        }

        public List<Rectangle> Rectangles => _rectangles.ToList();

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            if (rectangleSize.Width <= 0 || rectangleSize.Height <= 0)
                throw new ArgumentException("Ширина и высота прямоугольника должны быть положительными числами: " +
                                            $"{rectangleSize.Width} x {rectangleSize.Height}");

            var rectangle = _rectangles.Any()
                ? GetNextRectangleWithLocation(rectangleSize)
                : new Rectangle(Center.GetRectangleLocationByCenter(rectangleSize), rectangleSize);
            _rectangles.Add(rectangle);
            return rectangle;
        }

        public void Reset()
        {
            _rectangles.Clear();
            _curve.Reset();
        }

        private Rectangle GetNextRectangleWithLocation(Size rectSize)
        {
            var dryRect = new Rectangle(Point.Empty, rectSize);
            using var pointEnumerator = _curve.GetDiscretePoints().GetEnumerator();
            while (_rectangles.Any(r => r.IntersectsWith(dryRect)))
            {
                pointEnumerator.MoveNext();
                dryRect.Location = pointEnumerator.Current;
            }

            return dryRect;
        }
    }
}