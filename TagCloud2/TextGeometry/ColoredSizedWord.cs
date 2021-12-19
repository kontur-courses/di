using System.Drawing;

namespace TagCloud2.TextGeometry
{
    public class ColoredSizedWord : IColoredSizedWord
    {
        public Color Color { get; }

        public Rectangle Size { get; }

        public string Word { get; }

        public Font Font { get; }

        public ColoredSizedWord(Color color, Rectangle size, string word, Font font)
        {
            this.Color = color;
            this.Size = size;
            this.Word = word;
            this.Font = font;
        }

        public ColoredSizedWord(string word, Font font)
        {
            this.Word = word;
            this.Font = font;
        }
    }
}
