using System.Drawing;
using TagCloud.configurations;

namespace TagCloud
{
    public class Tag
    {
        private readonly string text;
        private readonly Color color;
        private readonly Font font;
        private readonly RectangleF layoutRectangle;

        public Tag(string text, Color color, Font font, RectangleF layoutRectangle)
        {
            this.text = text;
            this.color = color;
            this.font = font;
            this.layoutRectangle = layoutRectangle;
        }

        public string GetText() => text;

        public Color GetColor() => color;

        public Font GetFont() => font;

        public RectangleF GetLayoutRectangle() => layoutRectangle;
    }
}