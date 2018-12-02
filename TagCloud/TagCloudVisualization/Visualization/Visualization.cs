using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagCloud.Settings;
using TagCloud.TagCloudVisualization.Extensions;

namespace TagCloud.TagCloudVisualization.Visualization
{
    public abstract class Visualization
    {
        public ImageBox ImageBox;
        protected ImageSettings ImageSettings;
        protected Graphics Graphics;
        private Size bitmapSize { get; set; }
        protected IEnumerable<Rectangle> Rectangles;

        private void DetermineBitmapSizes()
        {
            var mostDistantRectangle = Rectangles
                .OrderByDescending(rect => rect.GetCircumcircleRadius())
                .FirstOrDefault();
            var circleRadius = mostDistantRectangle.GetCircumcircleRadius();
            var bitmapSide = Math.Max(circleRadius * 2, Math.Max(ImageSettings.Width, ImageSettings.Height));
            bitmapSize = new Size(bitmapSide, bitmapSide);
        }

        public Rectangle ShiftRectangleToCenter(Rectangle rect)
        {
            var layoutCenter = new Point(bitmapSize.Width / 2, bitmapSize.Height / 2);
            return new Rectangle(new Point(rect.X + layoutCenter.X, rect.Y + layoutCenter.Y), rect.Size);
        }

        public void GetTagCloudImage()
        {
            DetermineBitmapSizes();
            ImageSettings.Width = bitmapSize.Width;
            ImageSettings.Height = bitmapSize.Height;
            ImageBox.RecreateImage(ImageSettings);
            if (!Rectangles.Any())
                return;
            using (Graphics = ImageBox.StartDrawing())
            {
                Graphics.Clear(ImageSettings.BackdgoundColor);
                DrawElements();
            }
        }

        protected abstract void DrawElements();
    }
}
