using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagCloudTests
{
    public class RectanglesDrawer
    {
        private const int SideShift = 10;
        private const int MaximumSize = 4000;

        public void GenerateImage(IEnumerable<Rectangle> rectangles, string imageName)
        {
            var rectanglesArray = rectangles as Rectangle[] ?? rectangles.ToArray();
            var size = Math.Min(rectanglesArray.Sum(rectangle => Math.Max(rectangle.Height, rectangle.Width)) + SideShift, MaximumSize);
            var image = new Bitmap(size, size);
            var graphics = Graphics.FromImage(image);
            graphics.TranslateTransform(size / 2f, size / 2f);
            foreach (var rectangle in rectanglesArray)
                graphics.DrawRectangle(Pens.Black, rectangle);
            image.Save(imageName);
        }
    }
}