using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagCloud.Tests.Extensions;

namespace TagCloud.Tests.Layouters
{
    internal class RectanglesVisualizer
    {
        internal static Bitmap Visualize(IReadOnlyCollection<RectangleF> rectangles)
        {
            var (bitmapSize, offsetFromCenter) = GetNecessarySizeForBitmapAndOffset(rectangles);
            var center = new PointF((float)bitmapSize.Width / 2, (float)bitmapSize.Height / 2);
            var offsetForRectangles = center.Add(offsetFromCenter);

            var canvas = new Bitmap(bitmapSize.Width, bitmapSize.Height);
            var penForRectangles = new Pen(Color.Blue, 1);
            var resRectangles = rectangles
                .Select(r => new RectangleF(r.Location.Add(offsetForRectangles), r.Size))
                .ToArray();
            using (var graphics = Graphics.FromImage(canvas))
            {
                graphics.Clear(Color.Black);
                    graphics.DrawRectangles(penForRectangles, resRectangles);
            }

            return canvas;
        }

        private static (Size, PointF) GetNecessarySizeForBitmapAndOffset(IReadOnlyCollection<RectangleF> rectangles)
        {
            var leftTopCorner = new PointF(int.MaxValue, int.MaxValue);
            var rightBottomCorner = new PointF(int.MinValue, int.MinValue);

            foreach (var rectangle in rectangles)
            {
                if (rectangle.Bottom > rightBottomCorner.Y)
                    rightBottomCorner.Y = rectangle.Bottom;
                if (rectangle.Right > rightBottomCorner.X)
                    rightBottomCorner.X = rectangle.Right;
                if (rectangle.Top < leftTopCorner.Y)
                    leftTopCorner.Y = rectangle.Top;
                if (rectangle.Left < leftTopCorner.X)
                    leftTopCorner.X = rectangle.Left;
            }

            var width = rightBottomCorner.X - leftTopCorner.X + 1;
            var height = rightBottomCorner.Y - leftTopCorner.Y + 1;
            var dx = (rightBottomCorner.X + leftTopCorner.X + 1) / 2;
            var dy = (rightBottomCorner.Y + leftTopCorner.Y + 1) / 2;
            return (new Size((int)width + 1, (int)height + 1), new PointF(-dx, -dy));
        }
    }
}