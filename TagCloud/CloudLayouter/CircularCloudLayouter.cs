using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagCloud.CloudLayouter
{
    public class CircularCloudLayouter
    {
        private readonly ArchimedeanSpiral spiral;
        public HashSet<Rectangle> Rectangles;
        private readonly ImageSettings screenSize;
        private readonly SpiralSettings spiralSettings;

        public CircularCloudLayouter(ArchimedeanSpiral spiral, SpiralSettings spiralSettings,
            ImageSettings screenSize)
        {
            this.screenSize = screenSize;
            this.spiral = spiral;
            this.spiralSettings = spiralSettings;
            CheckCorrectScreenSize(screenSize);
            Rectangles = new HashSet<Rectangle>();
        }

        public void RefreshLayouter()
        {
            spiral.SetStartPoint(spiralSettings);
            Rectangles = new HashSet<Rectangle>();
        }

        private void CheckCorrectScreenSize(ImageSettings screenSize)
        {
            if (screenSize.Width <= 0
                || screenSize.Width > this.screenSize.Width
                || screenSize.Height <= 0
                || screenSize.Height > this.screenSize.Height)
                throw new ArgumentException("Incorrect size of screen " + "w:" + screenSize.Width + "h:" +
                                            screenSize.Height);
        }

        public Rectangle PutNextRectangle(Size rectangleSize, string word, int frequency)
        {
            CheckCorrectSize(rectangleSize);
            foreach (var point in spiral.GetNewPointLazy())
            {
                var rect = new Rectangle(point, rectangleSize);
                if (!CheckCorrectRectanglePosition(rect)) return new Rectangle(point, Size.Empty);
                if (!RectangleDoesNotIntersect(rect)) continue;
                Rectangles.Add(rect);
                return rect;
            }

            throw new Exception("Rectangle should be added anyway");
        }

        private void CheckCorrectSize(Size rectangleSize)
        {
            if (rectangleSize.Width <= 0
                || rectangleSize.Width > screenSize.Width
                || rectangleSize.Height <= 0
                || rectangleSize.Height > screenSize.Height)
                throw new ArgumentException("Incorrect size of rectangle " + "w:" + rectangleSize.Width + "h:" +
                                            rectangleSize.Height);
        }

        private bool CheckCorrectRectanglePosition(Rectangle rect)
        {
            return rect.Location.X + rect.Size.Width <= screenSize.Width / 2 &&
                   rect.Location.X >= -screenSize.Width / 2 &&
                   rect.Location.Y + rect.Size.Height <= screenSize.Height / 2 &&
                   rect.Location.Y >= -screenSize.Height / 2;
        }

        private bool RectangleDoesNotIntersect(Rectangle rectToAdd)
        {
            return Rectangles.All(rect => Rectangle.Intersect(rectToAdd, rect).IsEmpty);
        }
    }
}