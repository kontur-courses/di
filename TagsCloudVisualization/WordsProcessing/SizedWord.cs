using System.Drawing;

namespace TagsCloudVisualization.WordsProcessing
{
    public class SizedWord
    {
        public string Word { get; }
        public Font Font { get; }
        public Size Size { get; }

        public SizedWord(string word, Font font, Size size)
        {
            Word = word;
            Font = font;
            Size = size;
        }
    }
}