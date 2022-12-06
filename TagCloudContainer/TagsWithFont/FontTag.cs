using System.Drawing;

namespace TagCloudContainer.TagsWithFont
{
    public class FontTag
    {
        public string Word;
        public int SizeFont;
        public FontFamily Font;

        public FontTag(string word,int sizeFont, FontFamily font)
        {
            Word = word;
            SizeFont = sizeFont;
            Font = font;
        }

    }
}
