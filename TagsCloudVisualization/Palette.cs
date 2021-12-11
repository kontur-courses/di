using System.Drawing;

namespace TagsCloudVisualization
{
    public class Palette
    {
        public readonly Brush TextColor;
        public readonly Brush BackgroundColor;
        public readonly bool FillRectangles;

        internal static Palette DefaultPalette = new Palette(Color.Black);

        public Palette(Color textColors)
        {
            TextColor = new SolidBrush(textColors);
        }

        public Palette(Color textColor, Color backgroundColor)
            : this(textColor)
        {
            BackgroundColor = new SolidBrush(backgroundColor);
            FillRectangles = true;
        }
    }
}