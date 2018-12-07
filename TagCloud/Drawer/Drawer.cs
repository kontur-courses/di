using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagCloud.Data;

namespace TagCloud.Drawer
{
    public class Drawer : IWordsDrawer, IRectanglesDrawer
    {
        private const int SideShift = 10;
        private const int MaximumSize = 4000;

        public Bitmap CreateImage(IEnumerable<WordImageInfo> infos, Brush wordsBrush, Brush backgroundBrush)
        {
            var imageInfos = infos as WordImageInfo[] ?? infos.ToArray();
            var rectangles = imageInfos.Select(info => info.Rectangle).ToArray();

            var image = GetImage(rectangles);

            using (var graphics = Graphics.FromImage(image))
            {
                FillBitmap(backgroundBrush, graphics, image);
                MoveGraphicsToCenter(graphics, rectangles);
                foreach (var info in imageInfos)
                    graphics.DrawString(info.Word, info.Font, wordsBrush, info.Rectangle);
            }

            return image;
        }

        public Bitmap CreateImage(IEnumerable<Rectangle> rectangles)
        {
            var rectanglesArray = rectangles as Rectangle[] ?? rectangles.ToArray();

            var image = GetImage(rectanglesArray);

            using (var graphics = Graphics.FromImage(image))
            {
                FillBitmap(Brushes.White, graphics, image);
                MoveGraphicsToCenter(graphics, rectanglesArray);
                foreach (var rectangle in rectanglesArray)
                    graphics.DrawRectangle(Pens.Black, rectangle);
            }

            return image;
        }

        private static void FillBitmap(Brush backgroundBrush, Graphics graphics, Bitmap image)
        {
            graphics.FillRectangle(backgroundBrush, new Rectangle(0, 0, image.Width, image.Height));
        }

        private static void MoveGraphicsToCenter(Graphics graphics, Rectangle[] rectangles)
        {
            var left = GetLeftShift(rectangles);
            var top = GetTopShift(rectangles);

            var leftShift = -left + SideShift / 2f;
            var topShift = -top + SideShift / 2f;

            graphics.TranslateTransform(leftShift, topShift);
        }

        private static Bitmap GetImage(Rectangle[] rectangles)
        {
            var right = rectangles.Max(info => info.Right);
            var left = GetLeftShift(rectangles);
            var bottom = rectangles.Max(info => info.Bottom);
            var top = GetTopShift(rectangles);

            var width = Math.Min(right - left + SideShift, MaximumSize);
            var height = Math.Min(bottom - top + SideShift, MaximumSize);

            return new Bitmap(width, height);
        }

        private static int GetTopShift(IEnumerable<Rectangle> rectangles) => 
            rectangles.Min(info => info.Top);

        private static int GetLeftShift(IEnumerable<Rectangle> rectangles) =>
            rectangles.Min(info => info.Left);
    }
}