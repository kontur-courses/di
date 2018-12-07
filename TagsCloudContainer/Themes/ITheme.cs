using System.Drawing;

namespace TagsCloudContainer.Themes
{
    public interface ITheme
    {
        FontFamily FontFamily { get; }
        Brush BackgroundColor { get; }
        Brush WordColor { get; }
    }
}