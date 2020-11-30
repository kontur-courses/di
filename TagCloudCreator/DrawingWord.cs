using System.Drawing;

namespace TagCloudCreator
{
    public class DrawingWord
    {
        internal readonly Font Font;
        internal readonly string Word;
        internal Point Location;

        public DrawingWord(string word, Font font, Point location)
        {
            Word = word;
            Font = font;
            Location = location;
        }
    }
}