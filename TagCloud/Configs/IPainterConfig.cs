using System.Drawing;

namespace TagCloud
{
    public interface IPainterConfig
    {
        int ImageWidth { get; }
        int ImageHeight { get; }
        int MaxFontSize { get; }
        int MinFontSize { get; }
        string ImageName { get; }
        string PathForSave { get; }
        FontFamily FontFamily { get; }
        Point CloudCenter { get; }
    }
}