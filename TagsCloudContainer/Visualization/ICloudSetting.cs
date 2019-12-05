using System.Drawing;

namespace TagsCloudContainer.Visualization
{
    public interface ICloudSetting
    {
        Font Font { get; }
        Size ImageSize { get; }
        Color TextColor { get; }
        Color BackgroundColor { get; }
        Point GetCenterImage();
    }
}