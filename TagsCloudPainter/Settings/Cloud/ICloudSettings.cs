using System.Drawing;

namespace TagsCloudPainter.Settings.Cloud;

public interface ICloudSettings
{
    public Point CloudCenter { get; set; }
    public Color BackgroundColor { get; set; }
}