using System.Drawing;
using TagsCloudContainer.RectangleTranslation;

namespace TagsCloudContainer.Vizualization
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