using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;

namespace CircularCloudLayouter
{
    public static class RectanglePainter
    {
        public static bool TryDrawAndSaveToFile(Rectangle[] rectangles, Color penColor, string filePath)
        {
            if (rectangles.Length == 0)
                return false;

            var maxX = rectangles.Max(rect => rect.X);
            var minX = rectangles.Min(rect => rect.X);
            var maxY = rectangles.Max(rect => rect.Y);
            var minY = rectangles.Min(rect => rect.Y);

            var width = maxX - minX;
            var height = maxY - minY;

            var shiftVector = new Point(-minX, -minY);

            using var bitmap = new Bitmap(width + 100, height + 100);
            using var pen = new Pen(penColor);
            using var graphics = Graphics.FromImage(bitmap);
            
            graphics.DrawRectangles(pen, rectangles.Select(rectangle => rectangle.Shift(shiftVector)).ToArray());

            bitmap.Save(filePath, ImageFormat.Png);

            return true;
        }
    }
}