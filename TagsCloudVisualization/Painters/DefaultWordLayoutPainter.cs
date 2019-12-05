using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using TagsCloudVisualization.Core;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.Painters
{
    public class DefaultWordLayoutPainter : WordLayoutPainter
    {
        public DefaultWordLayoutPainter(ImageSettings imageSettings, Font font, Palette palette) : base(imageSettings, font, palette)
        {}

        public override Bitmap GetDrawnLayoutedWords(LayoutedWord[] layoutedWords)
        {
            var bitmap = new Bitmap(ImageSettings.Width, ImageSettings.Height);
            var graphics = Graphics.FromImage(bitmap);
            var backgroundBrush = new SolidBrush(Palette.BackgroundColor);
            var fontBrush = new SolidBrush(Palette.FontColor);
            var stringFormat = new StringFormat {LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center};
            graphics.FillRectangle(backgroundBrush, 0, 0, ImageSettings.Width, ImageSettings.Height);
            foreach (var layoutedWord in layoutedWords)
            {
                var font = GetScaledFontFor(graphics, layoutedWord);
                graphics.DrawString(layoutedWord.Word.Value, font, fontBrush, layoutedWord.Rectangle, stringFormat);
            }
            return bitmap;
        }

        private Font GetScaledFontFor(Graphics graphics, LayoutedWord layoutedWord)
        {
            var fontSize = graphics.MeasureString(layoutedWord.Word.Value, Font);
            var scaleUnit = layoutedWord.Rectangle.Size.Height / fontSize.Height;
            return new Font(Font.FontFamily, scaleUnit);
        }
    }
}