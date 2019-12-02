using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer.CloudVisualizers
{
    public interface ICloudVisualizer
    {
        Bitmap GetBitmap(IEnumerable<CloudVisualizationWord> words);
    }
}