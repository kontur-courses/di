using System.Drawing.Imaging;

namespace TagsCloudVisualization.ImageSavers;

public class JpegSaver : AbstractImageSaver
{
    protected override string Extension { get; } = "jpeg";
    protected override ImageFormat Format { get; } = ImageFormat.Jpeg;
}