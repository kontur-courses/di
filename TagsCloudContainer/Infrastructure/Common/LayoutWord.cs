using System.Drawing;

namespace TagsCloudContainer.Infrastructure.Common
{
    public class LayoutWord
    {
        public string Word { get; }
        public Brush Brush { get; }
        public Font Font { get; }
        
        public Size Size { get; }

        public LayoutWord(string word, Brush brush, Font font, Size size)
        {
            Word = word;
            Brush = brush;
            Font = font;
            Size = size;
        }
        
    }
}