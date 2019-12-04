using System;
using System.Drawing;

namespace TagsCloudVisualization
{
    public class Tag
    {
        public static readonly Color DefaultColor =  Color.Black;
        public WordToken WordToken { get; }
        public Rectangle TagBox { get; }
        public float FontSize
        {
            get => FontSize;
            set => FontSize = value >= 0 ? value : throw new ArgumentException();
        }

        public Color Color { get; set; }

        public Tag(WordToken wordToken, Rectangle tagBox, float fontSize)
        {
            WordToken = wordToken;
            TagBox = tagBox;
            FontSize = fontSize;
            Color = DefaultColor;
        }

        public Tag(WordToken wordToken, Rectangle tagBox, float fontSize, Color color) : this(wordToken, tagBox, fontSize)
        {
            Color = color;
        }
    }
}