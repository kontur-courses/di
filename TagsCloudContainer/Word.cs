using System.Drawing;
using TagsCloudContainer.WordFormatters;
using TagsCloudContainer.WordLayouts;

namespace TagsCloudContainer
{
    public class Word : IFormattedWord, IPositionedWord
    {
        public Font Font { get; }

        public Color Color { get; }

        public string Value { get; }

        public RectangleF Position { get; set; }

        public Word(Font font, Color color, string value)
        {
            Font = font;
            Color = color;
            Value = value;
        }
    }
}