using System.Drawing;

namespace TagCloud.Data
{
    public class WordImageInfo
    {
        public readonly string Word;
        public readonly Font Font;
        public readonly Rectangle Rectangle;
        public readonly float Frequency;

        public WordImageInfo(string word, Font font, Rectangle rectangle, float frequency)
        {
            Word = word;
            Font = font;
            Rectangle = rectangle;
            Frequency = frequency;
        }
    }
}