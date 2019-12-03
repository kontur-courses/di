using System.Drawing;

namespace TagsCloudVisualization
{
    public class Tag
    {
        public string Word;
        public Rectangle TagBox;
        public float FontSize;

        public Tag(string word, Rectangle tagBox, float fontSize)
        {
            Word = word;
            TagBox = tagBox;
            FontSize = fontSize;
        }
    }
}