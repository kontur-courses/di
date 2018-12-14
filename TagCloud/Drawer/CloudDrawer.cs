using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagCloud.ColorPicker;
using TagCloud.Data;

namespace TagCloud.Drawer
{
    public class CloudDrawer : IWordsDrawer, IRectanglesDrawer
    {
        private const int SideShift = 10;
        private const int MaximumSize = 4000;

        private readonly IColorPicker colorPicker;

        public static readonly HashSet<string> Colors = new HashSet<string>(
            typeof(Color)
                .GetProperties()
                .Where(color => color.PropertyType == typeof(Color))
                .Select(color => color.Name));

        public CloudDrawer(IColorPicker colorPicker)
        {
            this.colorPicker = colorPicker;
        }

        public Bitmap CreateImage(IEnumerable<WordImageInfo> infos, Color wordsColor, Color backgroundColor)
        {
            var imageInfos = infos as WordImageInfo[] ?? infos.ToArray();
            var rectangles = imageInfos.Select(info => info.Rectangle).ToArray();

            var image = GetImage(rectangles);

            using (var graphics = Graphics.FromImage(image))
            {
                FillBitmap(new SolidBrush(backgroundColor), graphics, image);
                MoveGraphicsToCenter(graphics, rectangles);
                foreach (var info in imageInfos)
                {
                    var brush = new SolidBrush(colorPicker.AdjustColor(wordsColor, info.Frequency));
                    graphics.DrawString(info.Word, info.Font, brush, info.Rectangle);
                }
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