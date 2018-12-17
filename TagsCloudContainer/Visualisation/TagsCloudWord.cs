using System.Drawing;

namespace TagsCloudContainer.Visualisation
{
    public class TagsCloudWord
    {
        public string Word { get; }
        public Rectangle Rectangle { get; }

        public TagsCloudWord(string word, Rectangle rectangle)
        {
            Word = word;
            Rectangle = rectangle;
        }
    }
}