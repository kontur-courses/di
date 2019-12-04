using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudVisualization.Infrastructure;

namespace TagsCloudVisualization
{
    public class CircularCloudLayouter
    {
        private readonly Rectangle pictureRectangle;
        private readonly Spiral spiral;
        private readonly List<Rectangle> rectangles = new List<Rectangle>();

        public CircularCloudLayouter(Point center, Size pictureSize)
        {
            if (CheckIfSizeIsIncorrect(pictureSize))
            {
                throw new ArgumentException($"{nameof(pictureSize)} was incorrect");
            }

            pictureRectangle = GeometryHelper.GetRectangleFromCenterPoint(center, pictureSize);
            spiral = new Spiral(center);
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            CheckRectangleSize(rectangleSize);

            var rectangle = FindSuitableRectangle(rectangleSize);

            HandleFoundRectangle(rectangle);
            return rectangle;
        }

        public IEnumerable<Rectangle> PutMultipleRectangles(IEnumerable<Size> sizes)
        {
            return sizes.Select(PutNextRectangle);
        }

        private void HandleFoundRectangle(Rectangle rectangle)
        {
            if (!pictureRectangle.Contains(rectangle))
            {
                throw new ArgumentException("rectangle can not be placed in picture");
            }
            rectangles.Add(rectangle);
        }

        private void CheckRectangleSize(Size rectangleSize)
        {
            if (CheckIfSizeIsIncorrect(rectangleSize))
                throw new ArgumentException($"{nameof(rectangleSize)} was incorrect");
        }

        private Rectangle FindSuitableRectangle(Size rectangleSize)
        {
            return spiral.GetPointsLazy()
                .Select(p => GeometryHelper.GetRectangleFromCenterPoint(p, rectangleSize))
                .First(current => !rectangles.Any(r => r.IntersectsWith(current)));
        }

        private bool CheckIfSizeIsIncorrect(Size size) => size.Width <= 0 || size.Height <= 0;
    }
}