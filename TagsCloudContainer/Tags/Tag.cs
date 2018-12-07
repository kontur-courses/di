using System.Drawing;

namespace TagsCloudContainer.Tags
{
    public class Tag
    {
        public Tag(int fontSize, FontFamily fontFamily, string word)
        {
            Font = new Font(fontFamily, fontSize);
            Word = word;
        }

        public string Word { get; }
        public Rectangle Rectangle { get; set; }
        public Font Font { get; }
    }
}