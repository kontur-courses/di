using System.Drawing;

namespace Visualization
{
    public class SizedWord
    {
        public string Word { get; }
        public Size WordSize => CalculateSize();
        public float FontSize { get; }
        
        private const float HeightFontCoefficient = 2.0f;
        private const float WidthFontCoefficient = 2.0f;

        public SizedWord(string word, float fontSize)
        {
            Word = word;
            FontSize = fontSize;
        }

        private Size CalculateSize()
        {
            var height = HeightFontCoefficient * FontSize;
            var width = WidthFontCoefficient * FontSize * Word.Length;
            return new Size((int) width, (int) height);
        }
    }
}