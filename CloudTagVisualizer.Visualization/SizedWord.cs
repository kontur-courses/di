using System.Drawing;

namespace Visualization
{
    public class SizedWord
    {
        public string Word { get; }
        public Size WordSize => CalculateSize();
        public float FontSize { get; }
        
        private const float HeightSizeCoefficient = 2.0f;
        private const float WidthSizeCoefficient = 2.0f;

        public SizedWord(string word, float fontSize)
        {
            Word = word;
            FontSize = fontSize;
        }

        private Size CalculateSize()
        {
            var height = HeightSizeCoefficient * FontSize;
            var width = WidthSizeCoefficient * FontSize * Word.Length;
            return new Size((int) width, (int) height);
        }
    }
}