using System.Collections.Generic;
using TagsCloudContainer.RectangleTranslation;

namespace TagsCloudContainer.Vizualization.Interfaces
{
    public interface IWordLayouter
    {
        List<WordRectangle> LayoutWords(IEnumerable<SizedWord> sizedWords);
    }
}