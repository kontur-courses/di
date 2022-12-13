using System.Drawing;

namespace TagCloud.Abstractions;

public interface ICloudDrawer
{
    Graphics Graphics { get; }
    FontFamily FontFamily { get; }
    int MaxFontSize { get; }
    int MinFontSize { get; }
    Bitmap Draw(IEnumerable<IDrawableTag> tags);
}