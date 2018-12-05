using System.Drawing;

namespace TagsCloudContainer.Tags
{
    public class Tag
    {
        public Tag(int fontSize, string font, string word)
        {
            FontSize = fontSize;
            Font = new Font(new FontFamily(font), fontSize);
            Word = word;
        }

        public int FontSize { get; }
        public string Word { get; }
        public Rectangle Rectangle { get; set; }
        public Font Font { get; }
    }
}