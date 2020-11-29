using System.Drawing;

namespace TagCloudCreator
{
    internal class Word
    {
        internal string word;
        internal Font font;
        internal Point location;
        internal Size size;

        public Word(string word, Font font, Point location, Size size)
        {
            this.word = word;
            this.font = font;
            this.location = location;
            this.size = size;
        }
    }
}