using System.Drawing;
using TagsCloudVisualization.Core;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.Drawers
{
    public class DefaultWordDrawer : WordDrawer
    {
        public DefaultWordDrawer(AppSettings appSettings) : base(appSettings)
        {
        }

        public override Bitmap GetDrawnLayoutedWords(PaintedWord[] paintedWords)
        {
            var bitmap = new Bitmap(appSettings.ImageSettings.Width, appSettings.ImageSettings.Height);
            var graphics = Graphics.FromImage(bitmap);
            var backgroundBrush = new SolidBrush(appSettings.Palette.BackgroundColor);
            var fontBrush = new SolidBrush(appSettings.Palette.FontColor);
            var stringFormat = new StringFormat {LineAlignment = StringAlignment.Center,
                Alignment = StringAlignment.Center};
            graphics.FillRectangle(backgroundBrush, 0, 0, appSettings.ImageSettings.Width,
                appSettings.ImageSettings.Height);
            foreach (var paintedWord in paintedWords)
            {
                var font = GetScaledFontFor(graphics, paintedWord);
                graphics.DrawString(paintedWord.Value, font, fontBrush, paintedWord.Position, stringFormat);
            }

            return bitmap;
        }

        private Font GetScaledFontFor(Graphics graphics, PaintedWord layoutedWord)
        {
            var fontSize = graphics.MeasureString(layoutedWord.Value, appSettings.Font);
            var scaleUnit = layoutedWord.Position.Size.Height / fontSize.Height;
            return new Font(appSettings.Font.FontFamily, scaleUnit);
        }
    }
}