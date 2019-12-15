using System.Drawing;

namespace TagCloudGenerator.GeneratorCore.Tags
{
    public class TagStyle
    {
        public static StringFormat TextFormat => new StringFormat
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center
        };

        public Color TextColor { get; }
        public Font Font { get; }

        public TagStyle(Color textColor, Font font)
        {
            TextColor = textColor;
            Font = font;
        }
    }
}