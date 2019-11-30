using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer.CloudVisualizers
{
    public interface ICloudVisualizer
    {
        Bitmap MakeBitmap(IEnumerable<CloudVisualizationWord> visualizationWords, string path);
    }
}