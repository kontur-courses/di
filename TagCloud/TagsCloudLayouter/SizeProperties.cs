using System.Drawing;

namespace TagsCloudLayouter;

public class SizeProperties
{
    public SizeProperties(Size imageSize)
    {
        ImageSize = imageSize;
    }

    public Size ImageSize { get; set; }
    public Point ImageCenter => new Point(ImageSize.Width, ImageSize.Height);
}