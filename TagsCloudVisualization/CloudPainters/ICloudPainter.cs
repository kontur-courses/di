using System.Collections.Generic;
using System.Drawing;
using TagsCloudVisualization.Visualization;

namespace TagsCloudVisualization.CloudPainters
{
    public interface ICloudPainter<in T>
    {
        Bitmap GetImage(IEnumerable<T> drawnComponents, VisualisingOptions visualisingOptions);
    }
}