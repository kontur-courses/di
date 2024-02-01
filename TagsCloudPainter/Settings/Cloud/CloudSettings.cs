using System.Drawing;

namespace TagsCloudPainter.Settings.Cloud;

public class CloudSettings : ICloudSettings
{
    public Point CloudCenter { get; set; }
    public Color BackgroundColor { get; set; }
}