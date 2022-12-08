using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TagsCloudVisualization
{
    public static class RectangleVisualizer
    {
        public static void DrawInFile(Point center, IEnumerable<Rectangle> rectangles, string filename)
        {
            var padding = 20;
            var bitmapSize = GetBitmapSize(center, rectangles, padding);

            using var bitmap = new Bitmap(bitmapSize.Width, bitmapSize.Height);
            using var graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.White);

            var backgroundBrush = new SolidBrush(Color.Aqua);
            var rectangleBorderWidth = 2;
            var borderPen = new Pen(new SolidBrush(Color.Black), rectangleBorderWidth);
           
            foreach(var rectangle in rectangles)
            {
                Rectangle shiftedRectangle = rectangle;
                shiftedRectangle.X += bitmapSize.Width / 2;
                shiftedRectangle.Y += bitmapSize.Height / 2;

                graphics.FillRectangle(backgroundBrush, shiftedRectangle);
                graphics.DrawRectangle(borderPen, shiftedRectangle);
            }

            bitmap.Save(filename);
        }

        private static Size GetBitmapSize(Point center, IEnumerable<Rectangle> rectangles, int padding)
        {
            Size size = new Size();

            foreach(var rectangle in rectangles)
            {
                int currentLength = Math.Max(rectangle.Right - center.X, center.X - rectangle.Left) * 2;
                if(currentLength > size.Width)
                    size.Width = currentLength;

                currentLength = Math.Max(rectangle.Bottom - center.Y, center.Y - rectangle.Top) * 2;
                if (currentLength > size.Height)
                    size.Height = currentLength;
            }

            size.Width += padding * 2;
            size.Height += padding * 2;

            return size;
        }
    }
}