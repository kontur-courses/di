using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagCloud.Layouters;
using TagCloud.Settings;

namespace TagCloud.Drawers
{
    public class TagDrawer : IDisposable, ITagDrawer
    {
        private IRectangleLayouter layouter;
        private DrawerSettings settings;
        
        private Bitmap bitmap;
        private Graphics graphics;
        private SolidBrush brush = new SolidBrush(Color.Black);

        private Random random = new Random();
        
        public TagDrawer(DrawerSettings settings, IRectangleLayouter layouter)
        {
            this.layouter = layouter;
            this.settings = settings;
            var imageSize = settings.ImageSize;
            bitmap = new Bitmap(imageSize.Width, imageSize.Height);
            graphics = Graphics.FromImage(bitmap);
            SetBackGroundColor(imageSize, settings.BackgroundColor);
        }

        public Bitmap DrawTagCloud(IReadOnlyCollection<TagInfo> tags)
        {
            foreach (var tag in tags.OrderByDescending(t => t.Proportion))
                DrawTag(tag);
            return bitmap;
        }

        private void DrawTag(TagInfo tag)
        {
            var font = GetFont(tag);
            var occupiedRectangle = layouter.PutNextRectangle(MeasureStringSize(tag.Value, font));
            DrawString(tag.Value, font, occupiedRectangle.Location);
        }
        
        private Font GetFont(TagInfo tag)
        {
            var fontSize = Convert.ToInt32((settings.MaxFontSize - settings.MinFontSize) 
                * tag.Proportion + settings.MinFontSize);
            return new Font(settings.FontFamily, fontSize);
        }

        private Size MeasureStringSize(string text, Font font)
        {
            return graphics.MeasureString(text, font).ToSize();
        }

        private void DrawString(string text, Font font, Point textPosition, Color? color = null)
        {
            SetBrushColor(color);
            graphics.DrawString(text, font, brush, textPosition);
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