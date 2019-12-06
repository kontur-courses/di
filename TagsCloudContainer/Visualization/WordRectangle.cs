using System.Drawing;
using TagsCloudContainer.RectangleTranslation;

namespace TagsCloudContainer.Visualization
{
    public class WordRectangle
    {
        public readonly SizedWord SizedWord;
        public readonly RectangleF Rectangle;

        public WordRectangle(SizedWord sizedWord, RectangleF rectangle)
        {
            SizedWord = sizedWord;
            Rectangle = rectangle;
        }
    }
}