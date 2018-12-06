using System.Drawing;

namespace TagsCloudVisualization
{
    public class Palette
    {
        public Color BackgroundColor { get; }
        public Brush SecondaryColor { get; }

        public Palette(Color backgroundColor, Brush secondaryColor)
        {
            BackgroundColor = backgroundColor;
            SecondaryColor = secondaryColor;
        }
    }
}