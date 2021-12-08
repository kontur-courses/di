using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer.Visualizer
{
    public interface IVisualizer
    {
        public Bitmap Visualize(Dictionary<string, int> freqDict);
    }
}