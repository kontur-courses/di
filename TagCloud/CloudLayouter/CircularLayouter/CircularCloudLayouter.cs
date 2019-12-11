using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagCloud.Visualization;

namespace TagCloud.CloudLayouter.CircularLayouter
{
    public class CircularCloudLayouter : ICloudLayouter
    {
        private readonly ArchimedeanSpiral spiral;
        public HashSet<Rectangle> Rectangles { get; set; }
        public ImageSettings LayouterSettings { get; }

        public CircularCloudLayouter(ArchimedeanSpiral spiral,
            ImageSettings layouterSettings)
        {
            LayouterSettings = layouterSettings;
            this.spiral = spiral;
            CheckCorrectSize(layouterSettings);
            Rectangles = new HashSet<Rectangle>();
        }

        public void ResetLayouter()
        {
            spiral.SetNewStartPoint();
            Rectangles = new HashSet<Rectangle>();
        }

        public bool TryPutNextRectangle(Size rectangleSize, out Rectangle outRectangle)
        {
            CheckCorrectSize(rectangleSize);
            foreach (var point in spiral.GetNewPointLazy())
            {
                outRectangle = new Rectangle(point, rectangleSize);
                if (!IsCorrectRectanglePosition(outRectangle)) return false;
                if (!RectangleDoesNotIntersect(outRectangle)) continue;
                Rectangles.Add(outRectangle);
                return true;
            }

            throw new InvalidOperationException("Rectangle should be added after foreach block");
        }

        private void CheckCorrectSize(ImageSettings settings)
        {
            CheckCorrectSize(settings.ImageSize);
        }

        private void CheckCorrectSize(Size rectangleSize)
        {
            if (rectangleSize.Width <= 0
                || rectangleSize.Width > LayouterSettings.ImageSize.Width
                || rectangleSize.Height <= 0
                || rectangleSize.Height > LayouterSettings.ImageSize.Height)
                throw new ArgumentException(
                    $"Incorrect size of rectangle. Width: {rectangleSize.Width}, Height: {rectangleSize.Height}");
        }

        private bool IsCorrectRectanglePosition(Rectangle rect)
        {
            return rect.Location.X + rect.Size.Width <= LayouterSettings.ImageSize.Width / 2 &&
                   rect.Location.X >= -LayouterSettings.ImageSize.Width / 2 &&
                   rect.Location.Y + rect.Size.Height <= LayouterSettings.ImageSize.Height / 2 &&
                   rect.Location.Y >= -LayouterSettings.ImageSize.Height / 2;
        }

        private bool RectangleDoesNotIntersect(Rectangle rectToAdd)
        {
            return Rectangles.All(rect => Rectangle.Intersect(rectToAdd, rect).IsEmpty);
        }
    }
}