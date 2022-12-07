using System.Drawing;

namespace TagCloud
{
    public interface IImageSettings
    {
        Size Size { get; }
        Color BackgroundColor { get; }
        FontFamily FontFamily { get; }
        int MaxFontSize { get; }
        int MinFontSize { get; }
        IWordColoring WordColoring { get; }
    }
}
