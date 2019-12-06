using System.Collections.Generic;

namespace TagsCloudContainer.Vizualization.Interfaces
{
    public interface IVisualizer
    {
        void Visualize(IEnumerable<WordRectangle> wordRectangles, string path);
    }
}