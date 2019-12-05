using System.Drawing;

namespace TagsCloudContainer
{
    public class TagRectangle
    {
        public string Value { get; }
        public Rectangle Rectangle { get; }
        public TagRectangle(string value, Rectangle rectangle)
        {
            Value = value;
            this.Rectangle = rectangle;
        }
    }
}