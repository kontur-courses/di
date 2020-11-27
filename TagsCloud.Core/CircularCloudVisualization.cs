using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloud.Core
{
    public static class CircularCloudVisualization
    {
        private static readonly Random Random = new Random();

        public static Bitmap CreateImage(IEnumerable<Rectangle> rectangles, int imageWidth, int imageHeight)
        {
            var image = new Bitmap(imageWidth, imageHeight);
            foreach (var rect in rectangles)
            {
                var color = Color.FromArgb(Random.Next(0, 255), Random.Next(0, 255), Random.Next(0, 255));
                Graphics.FromImage(image).FillRectangle(new SolidBrush(color), rect);
            }

            return image;
        }
    }
}