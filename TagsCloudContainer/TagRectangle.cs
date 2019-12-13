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
            Rectangle = rectangle;
        }

        public bool Equals(TagRectangle obj)
        {
            return Value == obj.Value && Rectangle.Equals(obj.Rectangle);
        }
    }
}