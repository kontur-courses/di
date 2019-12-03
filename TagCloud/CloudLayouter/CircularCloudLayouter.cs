using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagCloud.CloudLayouter
{
    public class CircularCloudLayouter : ICloudLayouter
    {
        private readonly ArchimedeanSpiral spiral;
        public HashSet<Rectangle> Rectangles { get; set; }
        public ImageSettings LayouterSize { get; }

        public CircularCloudLayouter(ArchimedeanSpiral spiral,
            ImageSettings layouterSize)
        {
            LayouterSize = layouterSize;
            this.spiral = spiral;
            IsCorrectSize(layouterSize);
            Rectangles = new HashSet<Rectangle>();
        }

        public void RefreshLayouter()
        {
            spiral.SetNewStartPoint();
            Rectangles = new HashSet<Rectangle>();
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            IsCorrectSize(rectangleSize);
            foreach (var point in spiral.GetNewPointLazy())
            {
                var rect = new Rectangle(point, rectangleSize);
                if (!IsCorrectRectanglePosition(rect)) continue;
                if (!RectangleDoesNotIntersect(rect)) continue;
                Rectangles.Add(rect);
                return rect;
            }

            throw new InvalidOperationException("Rectangle should be added after foreach block");
        }

        private void IsCorrectSize(ImageSettings settings)
        {
            IsCorrectSize(new Size(settings.Width, settings.Height));
        }

        private void IsCorrectSize(Size rectangleSize)
        {
            if (rectangleSize.Width <= 0
                || rectangleSize.Width > LayouterSize.Width
                || rectangleSize.Height <= 0
                || rectangleSize.Height > LayouterSize.Height)
                throw new ArgumentException(
                    $"Incorrect size of rectangle. Width: {rectangleSize.Width}, Height: {rectangleSize.Height}");
        }

        private bool IsCorrectRectanglePosition(Rectangle rect)
        {
            return rect.Location.X + rect.Size.Width <= LayouterSize.Width / 2 &&
                   rect.Location.X >= -LayouterSize.Width / 2 &&
                   rect.Location.Y + rect.Size.Height <= LayouterSize.Height / 2 &&
                   rect.Location.Y >= -LayouterSize.Height / 2;
        }

        private bool RectangleDoesNotIntersect(Rectangle rectToAdd)
        {
            return Rectangles.All(rect => Rectangle.Intersect(rectToAdd, rect).IsEmpty);
        }
    }
}