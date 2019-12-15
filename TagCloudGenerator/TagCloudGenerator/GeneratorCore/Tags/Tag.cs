using System.Drawing;

namespace TagCloudGenerator.GeneratorCore.Tags
{
    public struct Tag
    {
        public string Text { get; }
        public TagStyle Style { get; }
        public Rectangle TagBox { get; }

        public Tag(string text, TagStyle style, Rectangle tagBox)
        {
            Text = text;
            Style = style;
            TagBox = tagBox;
        }
    }
}