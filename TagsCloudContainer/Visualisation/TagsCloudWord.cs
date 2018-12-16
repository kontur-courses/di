using System.Drawing;

namespace TagsCloudContainer.Visualisation
{
    public class TagsCloudWord
    {
        public string Word { get; private set; }
        public Rectangle Rectangle { get; private set; }

        public TagsCloudWord(string word, Rectangle rectangle)
        {
            Word = word;
            Rectangle = rectangle;
        }
    }
}