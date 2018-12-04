using System.Drawing;

namespace TagsCloudContainer.Tags
{
    public class Tag
    {
        public int FontSize { get; set; }
        public string Word { get; set; }
        public Rectangle Rectangle { get; set; }
        public Font Font { get; }

        public Tag(int fontSize, string font, string word)
        {
            FontSize = fontSize;
            Font = new Font(new FontFamily(font), fontSize);
            Word = word;
        }

    }
}