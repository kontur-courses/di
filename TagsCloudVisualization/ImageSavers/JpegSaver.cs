using System.Drawing;
using System.Drawing.Imaging;

namespace TagsCloudVisualization.Abstractions;

public class JpegSaver : IImageSaver
{
    public override string Extension { get; } = "jpeg";
    public override ImageFormat Format { get; } = ImageFormat.Jpeg;
}