using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization
{
    public static class Drawer
    {
        public static Bitmap DrawImage(List<ICloudTag> rectangles, Point center)
        {
            CheckParameters(rectangles, center);

            var image = new Bitmap(center.X + GetDeltaX(rectangles), center.Y + GetDeltaY(rectangles));
            using var graphics = Graphics.FromImage(image);

            foreach (var rectangle in rectangles)
            {
                graphics.DrawString(rectangle.Text, new Font(FontFamily.GenericMonospace, 10), new SolidBrush(Color.Brown), rectangle.Size);//TODO font and size
            }

            return image;
        }

        private static void CheckParameters(List<ICloudTag> rectangles, Point center)
        {
            if (center.X < 0 || center.Y < 0)
                throw new ArgumentException("X or Y of center was negative");

            if (!rectangles.Any())
                throw new ArgumentException("The sequence contains no elements");
        }

        private static int GetDeltaX(List<ICloudTag> rectangles)
        {
            return rectangles.Select(elem => elem.Size.Right).Max();
        }

        private static int GetDeltaY(List<ICloudTag> rectangles)
        {
            return rectangles.Select(elem => elem.Size.Bottom).Max();
        }
    }
}