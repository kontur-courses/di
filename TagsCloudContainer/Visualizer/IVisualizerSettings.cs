using System.Drawing;
using System.Drawing.Imaging;

namespace TagsCloudContainer.Visualizer
{
    public interface IVisualizerSettings
    {
        Color Color { get; }
        int ImageWidth { get; }
        int ImageHeight { get; }
        ImageFormat ImageFormat { get; }
    }
}