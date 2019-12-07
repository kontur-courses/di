using System.Drawing;

namespace TagsCloudGenerator
{
    public interface ICloudColorPainter
    {
        Color GetTagShapeColor();
        Color GetTagTextColor();
        Color BackgroundColor { get; }
    }
}