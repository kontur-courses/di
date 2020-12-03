using System.Drawing;

namespace WordCloudGenerator
{
    public class OrderedChoicePalette : IPalette
    {
        private readonly Color[] colors;

        private int colorIndex = -1;

        public OrderedChoicePalette(Color[] colors, Color backgroundColor)
        {
            this.colors = colors;
            BackgroundColor = backgroundColor;
        }

        public Color BackgroundColor { get; }

        public Color GetNextColor()
        {
            colorIndex = (colorIndex + 1) % colors.Length;
            return colors[colorIndex];
        }
    }
}