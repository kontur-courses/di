using System.Drawing;

namespace TagsCloudContainer.Tag
{
    public class SimpleTag : ITag
    {
        public string Value { get; }
        public Font Font { get; }

        public SimpleTag(string value, Font font)
        {
            Value = value;
            Font = font;
        }
    }
}