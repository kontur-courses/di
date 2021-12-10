using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization.Visualization
{
    public interface IVisualizer
    {
        public Bitmap Visualize(IEnumerable<string> visualizingValues);
    }
}