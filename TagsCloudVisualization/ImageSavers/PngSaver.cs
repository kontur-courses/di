using System.Drawing;
using System.Drawing.Imaging;

namespace TagsCloudVisualization.Abstractions;

public class PngSaver : IImageSaver
{
    public override string Extension { get; } = "png";
    public override ImageFormat Format { get; } = ImageFormat.Png;
}