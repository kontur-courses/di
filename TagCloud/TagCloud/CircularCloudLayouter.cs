using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagCloud.Extensions;

namespace TagCloud
{
    public class CircularCloudLayouter : ICloudLayouter
    {
        public readonly Point Center;
        private readonly ISpiral _spiral;
        private readonly List<Rectangle> _rectangles = new();
        public List<Rectangle> Rectangles => _rectangles.ToList();

        public CircularCloudLayouter(ISpiral spiral) :this(Point.Empty, spiral)
        {
        }

        public CircularCloudLayouter(Point center, ISpiral spiral)
        {
            Center = center;
            _spiral = spiral;
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            if (rectangleSize.Width <= 0 || rectangleSize.Height <= 0)
                throw new ArgumentException($"Ширина и высота прямоугольника должны быть положительными числами: " +
                    $"{rectangleSize.Width} x {rectangleSize.Height}");

            var rectangle = _rectangles.Any()
                ? GetNextRectangleWithLocation(rectangleSize)
                : new Rectangle(Center.GetRectangleLocationByCenter(rectangleSize), rectangleSize);
            _rectangles.Add(rectangle);
            return rectangle;
        }

        private Rectangle GetNextRectangleWithLocation(Size rectSize)
        {
            var dryRect = new Rectangle(Point.Empty, rectSize);
            using var pointEnumerator = _spiral.GetDiscretePoints().GetEnumerator();
            while (_rectangles.Any(r => r.IntersectsWith(dryRect)))
            {
                pointEnumerator.MoveNext();
                dryRect.Location = pointEnumerator.Current;
            }
            return dryRect;
        }
    }
}
