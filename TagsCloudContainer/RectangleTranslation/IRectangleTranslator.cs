using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer.RectangleTranslation
{
    public interface IRectangleTranslator
    {
        IEnumerable<Rectangle> TranslateWordsToRectangles(Dictionary<string, int> countedWords);
    }
}