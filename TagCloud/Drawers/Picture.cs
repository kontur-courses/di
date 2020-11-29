using System;
using System.Drawing;
using System.IO;

namespace TagCloud.Drawers
{
    public class Picture : IDisposable, IPicture
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

        public Size MeasureStringSize(string text, Font font)
        {
            return graphics.MeasureString(text, font).ToSize();
        }

        public void FillRectangle(Rectangle rectangle, Color? color = null)
        {
            SetBrushColor(color);
            graphics.FillRectangle(brush, rectangle);
        }
        
        // Оставил этот метод на случай вдруг пригодится
        public void DrawCircle(Point position, float radius, Color? color = null)
        {
            SetBrushColor(color);
            graphics.DrawEllipse(new Pen(brush), 
                position.X - radius, position.Y - radius, 
                2 * radius, 2 * radius);
        }

        public void DrawString(string text, Font font, Point textPosition, Color? color = null)
        {
            SetBrushColor(color);
            graphics.DrawString(text, font, brush, textPosition);
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