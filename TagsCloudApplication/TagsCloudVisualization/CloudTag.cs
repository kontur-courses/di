using System.Drawing;

namespace TagsCloudVisualization
{
    public class CloudTag
    {
        public readonly string Word;
        public readonly Font Font;

        public CloudTag(string word, Font font)
        {
            Word = word;
            Font = font;
        }
    }
}
