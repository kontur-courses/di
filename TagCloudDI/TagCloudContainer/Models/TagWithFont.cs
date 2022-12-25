using System.Drawing;
using TagCloudContainer.Interfaces;

namespace TagCloudContainer.Models
{
    public class TagWithFont : ITag
    {
        public string Word { get; }
        public int SizeFont { get; }
        public FontFamily Font { get; }

        public TagWithFont(string word, int sizeFont, FontFamily font)
        {
            Word = word;
            SizeFont = sizeFont;
            Font = font ?? throw new ArgumentNullException(nameof(font));
        }
    }
}
