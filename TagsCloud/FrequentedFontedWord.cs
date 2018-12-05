using System.Drawing;

namespace TagsCloudVisualization
{
    public class FrequentedFontedWord
    {
        public int Frequency { get; }
        public string Word { get; }
        public Font Font { get; }

        public FrequentedFontedWord(string word, int frequency, Font font)
        {
            Word = word;
            Frequency = frequency;
            Font = font;
        }
    }
}