using System.Drawing;
using TagsCloudGenerator.CloudLayouter;

namespace TagsCloudGenerator.Visualizer
{
    public interface ICloudVisualizer
    {
        Bitmap Draw(Cloud cloud, ImageSettings settings);
    }
}