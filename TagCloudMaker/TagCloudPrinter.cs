using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace TagCloudMaker
{
    class TagCloudPrinter
    {
        public static void PrintTagCloud(TextRectangle[] rectangles)
        {
            var left = rectangles.Select(tr => tr.Rectangle).Min(r => r.Left);
            var top = rectangles.Select(tr => tr.Rectangle).Min(r => r.Top);
            rectangles = rectangles.Select(tr =>
            {
                tr.SetLocation(new Point(tr.Rectangle.X - left, tr.Rectangle.Y - top));
                return tr;
            }).ToArray();
            var canvasBitmap = new Bitmap(rectangles.Select(tr => tr.Rectangle).Max(r => r.Bottom), 
                                          rectangles.Select(tr => tr.Rectangle).Max(r => r.Right));
            var pen = new Pen(Color.Black);
            var testPictureName = Path.GetRandomFileName() + ImageFormat.Png;
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, testPictureName);
            var canvas = Graphics.FromImage(canvasBitmap);

            canvas.Clear(Color.White);
            foreach (var rectangle in rectangles)
            {
                canvas.DrawString(rectangle.Text, Font.FromHdc(IntPtr.Zero), new SolidBrush(Color.Black), rectangle.Rectangle);
            }
            canvas.Save();

            canvasBitmap.Save(path, ImageFormat.Png);
            Console.WriteLine($"Image was saved in {path}.");
        }
    }
}
