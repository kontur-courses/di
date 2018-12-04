using System.Drawing;

namespace TagsCloudVisualization
{
    public class TagsCloudWord
    {
        public string Word { get; set; }
        public Rectangle Rectangle { get; set; }

        public TagsCloudWord(string word, Rectangle rectangle)
        {
            Word = word;
            Rectangle = rectangle;
        }
    }
}