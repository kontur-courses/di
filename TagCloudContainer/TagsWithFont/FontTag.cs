using System.Drawing;

namespace TagCloudContainer.TagsWithFont
{
    public class FontTag : ITag
    {
        public string Word { get; }
        public int SizeFont { get; }
        public FontFamily Font { get; }

        public FontTag(string word, int sizeFont, FontFamily font)
        {
            Word = word;
            SizeFont = sizeFont;
            Font = font;
        }

    }
}
