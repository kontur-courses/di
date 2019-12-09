using System;
using System.Drawing;
using TagsCloudGenerator.Attributes;
using TagsCloudGenerator.Interfaces;

namespace TagsCloudGenerator.Painters
{
    [Factorial("PainterWithUserColors")]
    public class PainterWithUserColors : IPainter
    {
        private readonly IPainterSettings painterSettings;

        public PainterWithUserColors(IPainterSettings painterSettings) =>
            this.painterSettings = painterSettings;

        public void DrawWords(
            (string word, float maxFontSymbolWidth, string fontName, RectangleF wordRectangle)[] layoutedWords,
            Graphics graphics)
        {
            var colors = painterSettings.Colors;
            var backgroundColor = painterSettings.BackgroundColor;
            if (layoutedWords == null)
                throw new ArgumentNullException(nameof(layoutedWords));
            if (graphics == null)
                throw new ArgumentNullException(nameof(graphics));
            graphics.Clear(backgroundColor);
            var count = 0;
            foreach (var (word, maxFontSymbolWidth, fontName, rect) in layoutedWords)
                using (var font = new Font(fontName, maxFontSymbolWidth))
                    graphics.DrawString(
                        word,
                        font,
                        new SolidBrush(colors[count = ++count % colors.Length]),
                        rect);
        }
    }
}