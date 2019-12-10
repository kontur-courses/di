using System.Drawing;
using TagsCloudVisualization.Visualization;

namespace TagsCloudVisualization.CloudPainters
{
    public interface ICloudPainter
    {
        Bitmap GetImage(CloudComponents cloudComponents, VisualisingOptions visualisingOptions);
    }
}