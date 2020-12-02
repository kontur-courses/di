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
        private DrawerSettings settings;

        private Random random = new Random();
        
        public TagDrawer(Size pictureSize, DrawerSettings settings)
        {
            this.settings = settings;
            bitmap = new Bitmap(pictureSize.Width, pictureSize.Height);
            graphics = Graphics.FromImage(bitmap);
            SetBackGroundColor(pictureSize);
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
        
        private void SetBackGroundColor(Size pictureSize)
        {
            FillRectangle(new Rectangle(new Point(0, 0), pictureSize),
                settings.BackGroundColor);
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