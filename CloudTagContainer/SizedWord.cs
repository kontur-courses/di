using System.Drawing;

namespace Visualization
{
    public class SizedWord
    {
        public string Word { get; }
        public Size WordSize => CalculateSize();
        public float FontSize { get; }
        
        private const float HeightCoefficient = 1.6f;

        public SizedWord(string word, float fontSize)
        {
            Word = word;
            FontSize = fontSize;
        }

        private Size CalculateSize()
        {
            var height = HeightCoefficient * FontSize;
            var width = FontSize * Word.Length;
            return new Size((int) width, (int) height);
        }
    }
}