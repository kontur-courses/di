using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using TagCloud.Settings;

namespace TagCloud.Drawers
{
    public class TagDrawer : IDisposable, ITagDrawer
    {
        private Bitmap bitmap;
        private Graphics graphics;
        private SolidBrush brush = new SolidBrush(Color.Black);

        private Random random = new Random();
        
        public TagDrawer(DrawerSettings settings)
        {
            var imageSize = settings.ImageSize;
            bitmap = new Bitmap(imageSize.Width, imageSize.Height);
            graphics = Graphics.FromImage(bitmap);
            SetBackGroundColor(imageSize, settings.BackgroundColor);
        }

        public Size MeasureStringSize(string text, Font font)
        {
            return graphics.MeasureString(text, font).ToSize();
        }

        public Bitmap DrawString(string text, Font font, Point textPosition, Color? color = null)
        {
            SetBrushColor(color);
            graphics.DrawString(text, font, brush, textPosition);
            return (Bitmap)bitmap.Clone();
        }
        
        private void SetBackGroundColor(Size pictureSize, Color backgroundColor)
        {
            FillRectangle(new Rectangle(new Point(0, 0), pictureSize),
                backgroundColor);
        }
        
        private void FillRectangle(Rectangle rectangle, Color? color = null)
        {
            SetBrushColor(color);
            graphics.FillRectangle(brush, rectangle);
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
            bitmap?.Dispose();
            graphics?.Dispose();
            brush?.Dispose();
        }
    }
}