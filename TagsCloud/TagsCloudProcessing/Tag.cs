using System.Drawing;

namespace TagsCloud.TagsCloudProcessing
{
    public class Tag
    {
        public string Value { get; set; }
        public Rectangle Rectangle { get; set; }
        public Font Font { get; set; }

        public Tag(string value, Rectangle rectangle, Font font)
        {
            Value = value;
            Rectangle = rectangle;
            Font = font;
        }
    }
}
