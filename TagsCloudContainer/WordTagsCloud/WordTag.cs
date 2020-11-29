using System.Drawing;

namespace TagsCloudContainer.WordTagsCloud
{
    public class WordTag
    {
        public readonly string Text;
        public readonly Rectangle Rectangle;

        public WordTag(string text, Rectangle rectangle)
        {
            Text = text;
            Rectangle = rectangle;
        }
    }
}