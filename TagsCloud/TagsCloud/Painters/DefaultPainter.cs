using System;
using System.Drawing;
using TagsCloudGenerator.Interfaces;

namespace TagsCloudGenerator.Painters
{
    public class DefaultPainter : IPainter
    {
        private readonly Color[] colors;
        private readonly Color backgroundColor;

        public DefaultPainter(Color[] colors, Color backgroundColor)
        {
            this.colors = colors ?? throw new ArgumentNullException(nameof(colors));
            if (colors.Length < 1)
                throw new ArgumentOutOfRangeException(nameof(colors));
            this.backgroundColor = backgroundColor;
        }

        public void DrawWords((string word, Font font, RectangleF wordRectangle)[] layoutedWords, Graphics graphics)
        {
            if (layoutedWords == null)
                throw new ArgumentNullException(nameof(layoutedWords));
            if (graphics == null)
                throw new ArgumentNullException(nameof(graphics));
            graphics.Clear(backgroundColor);
            var count = 0;
            foreach (var (word, font, rect) in layoutedWords)
                graphics.DrawString(word, font, new SolidBrush(colors[count++ % colors.Length]), rect);
        }
    }
}