using System.Collections.Generic;

namespace TagsCloudContainer.Visualization.Interfaces
{
    public interface IVisualizer
    {
        void Visualize(IEnumerable<WordRectangle> wordRectangles, string path);
    }
}