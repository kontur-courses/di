using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer.CloudVisualizers
{
    public interface IBitmapMaker
    {
        Bitmap MakeBitmap(IEnumerable<CloudVisualizationWord> words, CloudVisualizerSettings settings);
    }
}