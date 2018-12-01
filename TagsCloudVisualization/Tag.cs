using System.Drawing;

namespace TagsCloudVisualization
{
    public class Tag
    {
        public readonly string Word;
        public readonly Rectangle Location;
        public readonly Font Font;

        public Tag(string word, Rectangle location, Font font)
        {
            Word = word;
            Location = location;
            Font = font;
        }
    }
}
