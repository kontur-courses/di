using System.Drawing.Imaging;

namespace TagsCloudVisualization.ImageSavers;

public class PngImageSaver : AbstractImageSaver
{
    protected override string Extension { get; } = "png";
    protected override ImageFormat Format { get; } = ImageFormat.Png;
}