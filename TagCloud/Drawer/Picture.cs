using System;
using System.Drawing;
using System.IO;

namespace TagCloud.Drawer
{
    public class Picture : IDisposable
    {
        private Bitmap pictureBitmap;
        private Graphics graphics;
        private SolidBrush brush = new SolidBrush(Color.Black);

        private Random random = new Random();
        
        public Picture(Size pictureSize)
        {
            pictureBitmap = new Bitmap(pictureSize.Width, pictureSize.Height);
            graphics = Graphics.FromImage(pictureBitmap);
        }

        public void FillRectangle(Rectangle rectangle, Color? color = null)
        {
            SetBrushColor(color);
            graphics.FillRectangle(brush, rectangle);
        }

        public void DrawTag(TagInfo tag, Color? color = null)
        {
            SetBrushColor(color);
            graphics.DrawString(tag.Value, tag.Font, brush, tag.Rectangle.Location);
        }
        
        public void DrawCircle(Point position, float radius, Color? color = null)
        {
            SetBrushColor(color);
            graphics.DrawEllipse(new Pen(brush), 
                position.X - radius, position.Y - radius, 
                2 * radius, 2 * radius);
        }

        public void Save(string path = null, string outputFileName = "output")
        {
            path ??= Directory.GetCurrentDirectory();
            var fileName = $"{path}/{outputFileName}.bmp";
            pictureBitmap.Save(fileName);
            Console.WriteLine($"Tag cloud visualization saved to file {fileName}");
        }

        private void SetBrushColor(Color? color = null)
        {
            var brushColor = color ?? GetRandomColor();
            brush.Color = brushColor;
        }

        // TODO: сделать опционально
        private Color GetRandomColor()
        {
            return Color.FromArgb(
                random.Next(0, 256),
                random.Next(0, 256),
                random.Next(0, 256)
            );
        }

        public void Dispose()
        {
            pictureBitmap?.Dispose();
            graphics?.Dispose();
            brush?.Dispose();
        }
    }
}