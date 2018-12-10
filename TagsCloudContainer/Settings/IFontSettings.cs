using System.Drawing;

namespace TagsCloudContainer.Settings
{
    public interface IFontSettings
    {
        FontFamily FontFamily { get; set; }
        Brush Brush { get; set; }
    }
}
