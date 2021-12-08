using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization.Visualization
{
    public interface IVisualizer<in T>
    {
        public Bitmap Visualize(IEnumerable<T> visualizingValues);
    }
}