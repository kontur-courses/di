using System.Collections.Generic;
using System.Drawing;
using TagsCloudVisualization.Visualization;

namespace TagsCloudVisualization.CloudPainters
{
    public interface ICloudPainter
    {
        Bitmap GetImage(IEnumerable<string> words, IEnumerable<Rectangle> rectangles,
            VisualisingOptions visualisingOptions);
    }
}