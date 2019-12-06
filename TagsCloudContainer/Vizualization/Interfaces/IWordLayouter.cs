using System.Collections.Generic;
using TagsCloudContainer.RectangleTranslation;

namespace TagsCloudContainer.Vizualization
{
    public interface IWordLayouter
    {
        List<WordRectangle> LayoutWords(IEnumerable<SizedWord> sizedWords);
    }
}