using System.Drawing;
using System.Linq;
using TagsCloudVisualization;

namespace TagCloudTests
{
    public static class RectangleCloudVisualiser
    {
        public static Bitmap DrawCloud(CircularCloudMaker maker, Rectangle frame, Size bitmapSize)
        {
            var image = new Bitmap(bitmapSize.Width, bitmapSize.Height);
            var graphics = Graphics.FromImage(image);
            graphics.ScaleTransform((float)bitmapSize.Width / frame.Width, 
                                    (float)bitmapSize.Height / frame.Height);
            graphics.TranslateTransform(-frame.Location.X, -frame.Location.Y);
            graphics.FillRectangles(Brushes.Red, maker.Rectangles.ToArray());
            graphics.DrawRectangles(new Pen(Color.Black, (float)frame.Width / bitmapSize.Width * 4), maker.Rectangles.ToArray());
            return image;
        }

        public static Bitmap DrawCloud(CircularCloudMaker maker, Size bitmapSize)
        {
            var radius = (int)(maker.Radius * 1.1);
            return DrawCloud(maker,new Rectangle(-radius, -radius, radius * 2, radius * 2), bitmapSize);
        }
    }
}