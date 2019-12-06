using System.Collections.Generic;
using TagsCloudContainer.RectangleTranslation;

namespace TagsCloudContainer
{
    public interface IVisualizer
    {
        void Visualize(IEnumerable<WordRectangle> wordRectangles, string path);
    }
}