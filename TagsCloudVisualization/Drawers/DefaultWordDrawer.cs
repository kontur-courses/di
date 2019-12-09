using System.Drawing;
using TagsCloudVisualization.Core;
using TagsCloudVisualization.Painters;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.Drawers
{
    public class DefaultWordDrawer : WordDrawer
    {
        public DefaultWordDrawer(ImageSettings imageSettings, Font font, Palette palette) : base(imageSettings, font, palette)
        { }

        public override Bitmap GetDrawnLayoutedWords(PaintedWord[] paintedWords)
        {
            var bitmap = new Bitmap(ImageSettings.Width, ImageSettings.Height);
            var graphics = Graphics.FromImage(bitmap);
            var backgroundBrush = new SolidBrush(Palette.BackgroundColor);
            var fontBrush = new SolidBrush(Palette.FontColor);
            var stringFormat = new StringFormat { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center };
            graphics.FillRectangle(backgroundBrush, 0, 0, ImageSettings.Width, ImageSettings.Height);
            foreach (var paintedWord in paintedWords)
            {
                var font = GetScaledFontFor(graphics, paintedWord);
                graphics.DrawString(paintedWord.Value, font, fontBrush, paintedWord.Position, stringFormat);
            }
            return bitmap;
        }

        private Font GetScaledFontFor(Graphics graphics, PaintedWord layoutedWord)
        {
            var fontSize = graphics.MeasureString(layoutedWord.Value, Font);
            var scaleUnit = layoutedWord.Position.Size.Height / fontSize.Height;
            return new Font(Font.FontFamily, scaleUnit);
        }
    }
}