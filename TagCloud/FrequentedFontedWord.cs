using System.Drawing;

namespace TagCloud
{
    public class FrequentedFontedWord
    {
        public FrequentedFontedWord(string word, int frequency, Font font)
        {
            Word = word;
            Frequency = frequency;
            Font = font;
        }

        public int Frequency { get; }
        public string Word { get; }
        public Font Font { get; }
    }
}