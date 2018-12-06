using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization
{
    public class RectanglesRenderer
    {
        public static Image GenerateImage(IEnumerable<Rectangle> rectangles, float lineWidth = 6f)
        {
            var maxX = 0;
            var maxY = 0;
            var maxWidth = 0;
            var maxHeight = 0;

            var listOfRectangles = rectangles.ToList();
            foreach (var rectangle in listOfRectangles)
            {
                maxX = Math.Max(maxX, rectangle.X);
                maxY = Math.Max(maxY, rectangle.Y);
                maxWidth = Math.Max(maxWidth, rectangle.Width);
                maxHeight = Math.Max(maxHeight, rectangle.Height);
            }

            var image = new Bitmap(maxX + maxWidth + 500, maxY + maxHeight + 500);
            var graphics = Graphics.FromImage(image);
            listOfRectangles.ToList().ForEach(r => graphics.FillRectangle(Brushes.DarkCyan, r));
            listOfRectangles.ToList().ForEach(r => graphics.DrawRectangle(new Pen(Color.Black, 6f), r));
            return image;
        }
    }
}
