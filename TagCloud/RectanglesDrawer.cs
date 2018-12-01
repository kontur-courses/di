using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagCloud
{
    public class RectanglesDrawer : IDrawer
    {
        private const int SideShift = 10;
        private const int MaximumSize = 4000;

        public void GenerateImage(IEnumerable<Rectangle> rectangles, string imageName)
        {
            var rectanglesArray = rectangles as Rectangle[] ?? rectangles.ToArray();
            var size = Math.Min(rectanglesArray.Sum(rectangle => Math.Max(rectangle.Height, rectangle.Width)) + SideShift, MaximumSize);
            var image = new Bitmap(size, size);
            var graphics = Graphics.FromImage(image);
            graphics.TranslateTransform(size / 2, size / 2);
            foreach (var rectangle in rectanglesArray)
                graphics.DrawRectangle(Pens.Black, rectangle);
            image.Save(imageName);
        }
    }
}