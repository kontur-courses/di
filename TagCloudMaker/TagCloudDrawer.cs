using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace TagCloud
{
    public class TagCloudDrawer
    {
        public static string DrawTagCloud(IEnumerable<TextRectangle> rectangles, Color background, Color brushColor, Font font,
            Size imageSize, ImageFormat imageFormat)
        {
            var movedRectangles = MoveRectanglesUpAndLeft(rectangles);

            var heightCoeff = imageSize.Height * movedRectangles.Select(tr => tr.Rectangle).Max(r => r.Bottom);
            var widthCoeff = imageSize.Width * movedRectangles.Select(tr => tr.Rectangle).Max(r => r.Right);

            var preparedRectangles = rectangles.Select(tr =>
                {
                    tr.ChangeSize(widthCoeff, heightCoeff);
                    return tr;
                }).ToArray();

            var pictureName = Path.GetRandomFileName() + ImageFormat.Png;
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, pictureName);

            var canvasBitmap = new Bitmap(imageSize.Width, imageSize.Height);
            var canvas = Graphics.FromImage(canvasBitmap);
            canvas.Clear(background);

            foreach (var rectangle in preparedRectangles)
            {
                canvas.DrawString(rectangle.Text, font, new SolidBrush(brushColor), rectangle.Rectangle);
            }

            canvas.Save();
            canvasBitmap.Save(path, imageFormat);

            return $"Image was saved in {path}.";
        }

        private static IEnumerable<TextRectangle> MoveRectanglesUpAndLeft(IEnumerable<TextRectangle> rectangles)
        {
            var left = rectangles.Select(tr => tr.Rectangle).Min(r => r.Left);
            var top = rectangles.Select(tr => tr.Rectangle).Min(r => r.Top);

            return rectangles.Select(tr =>
            {
                tr.SetLocation(new Point(tr.Rectangle.X - left, tr.Rectangle.Y - top));
                return tr;
            }).ToArray();
        }
    }
}
