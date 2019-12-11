using System.Drawing;

namespace TagCloud.Models
{
    public class Tag
    {
        public Tag(string text, int count, Font font)
        {
            Count = count;
            Text = text;
            Font = font;
        }

        public int Count { get; }
        public string Text { get; }
        public Font Font { get; }
    }
}