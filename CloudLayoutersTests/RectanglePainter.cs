using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace CloudLayouters
{
    public static class RectanglePainter
    {
        public static void DrawRectanglesInFile(IEnumerable<Rectangle> rectangles, string path = "",
            Size? imageSize = null)
        {
            imageSize ??= new Size(2000, 2000);
            var image = new Bitmap(imageSize.Value.Width, imageSize.Value.Height);
            var g = Graphics.FromImage(image);
            g.FillRectangle(new SolidBrush(Color.White), new Rectangle(0, 0, 2000, 2000));
            foreach (var rectangle in rectangles)
            {
                g.FillRectangle(new SolidBrush(Color.Chartreuse), rectangle);
                g.DrawRectangle(new Pen(Color.Red, 3), rectangle);
            }

            g.Save();
            image.Save(path + "visualisation.png", ImageFormat.Png);
        }
    }
}